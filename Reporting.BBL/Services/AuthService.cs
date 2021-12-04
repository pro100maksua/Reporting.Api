using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Reporting.BBL.Infrastructure;
using Reporting.BBL.Interfaces;
using Reporting.Common.Constants;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;
using Reporting.Domain.Interfaces;

namespace Reporting.BBL.Services
{
    public class AuthService : IAuthService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISimpleRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork,
            ISimpleRepository repository,
            IConfiguration configuration,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<string> Login(LoginDto dto)
        {
            var passwordHash = CryptoHelper.GetMd5Hash(dto.Password);
            var user = await _repository.Get<User>(e => e.Email == dto.Email, new[] { nameof(User.Roles) });

            if (user == null || user.Password != passwordHash)
            {
                return null;
            }

            return await GenerateToken(user);
        }

        public async Task<ResponseDto<string>> Register(RegisterDto dto)
        {
            var response = new ResponseDto<string>();

            var errorMessage = await ValidateUserEmail(dto.Email);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                response.ErrorMessage = errorMessage;
                return response;
            }

            var user = _mapper.Map<User>(dto);

            user.Password = CryptoHelper.GetMd5Hash(dto.Password);

            var role = await _repository.Get<Role>(dto.RoleId);
            user.Roles.Add(role);

            await _repository.Add<User>(user);
            await _unitOfWork.SaveChanges();

            response.Value = await GenerateToken(user);
            return response;
        }

        public async Task<string> ValidateEmail(ValidateValueDto dto)
        {
            var errorMessage = await ValidateUserEmail(dto.Value, dto.Id);

            return string.IsNullOrWhiteSpace(errorMessage) ? null : errorMessage;
        }

        public async Task<ResponseDto<string>> UpdateLoggedInUser(RegisterDto dto)
        {
            var response = new ResponseDto<string>();

            var id = int.Parse(_currentUserService.UserId);

            var errorMessage = await ValidateUserEmail(dto.Email, id);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                response.ErrorMessage = errorMessage;
                return response;
            }

            var user = await _repository.Get<User>(e => e.Id == id, new[] { nameof(User.Roles) });

            _mapper.Map(dto, user);

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.Password = CryptoHelper.GetMd5Hash(dto.Password);
            }

            foreach (var item in user.Roles.ToList())
            {
                user.Roles.Remove(item);
            }

            var role = await _repository.Get<Role>(dto.RoleId);
            user.Roles.Add(role);

            await _unitOfWork.SaveChanges();

            response.Value = await GenerateToken(user);
            return response;
        }

        public async Task<UserDto> GetLoggedInUser()
        {
            var id = int.Parse(_currentUserService.UserId);
            var user = await _repository.Get<User>(e => e.Id == id, new[] { nameof(User.Roles) });

            return _mapper.Map<UserDto>(user);
        }

        private async Task<string> GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration[AppConstants.Secret]);

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Name),
            };

            var roles = await _repository.GetAll<Role>();
            var userRoles = roles.Where(e => e.Value <= user.Roles.Max(r => r.Value));
            claims.AddRange(userRoles.Select(e => new Claim(ClaimTypes.Role, e.Value.ToString())));

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescription));
            return token;
        }

        private async Task<string> ValidateUserEmail(string email, int? id = null)
        {
            var users = await _repository.GetAll<User>();

            if (users.Any(e => e.Id != id && string.Equals(e.Email, email, StringComparison.OrdinalIgnoreCase)))
            {
                return "Дана пошта вже зайнята";
            }

            return null;
        }
    }
}
