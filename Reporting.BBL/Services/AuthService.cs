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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Role> _rolesRepository;
        private readonly IRepository<User> _usersRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, IRepository<Role> rolesRepository,
            IRepository<User> usersRepository, IConfiguration configuration, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _rolesRepository = rolesRepository;
            _usersRepository = usersRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<string> Login(LoginDto dto)
        {
            var passwordHash = CryptoHelper.GetMd5Hash(dto.Password);
            var user = await _usersRepository.Get(e => e.Email == dto.Email, new[] { nameof(User.Roles) });

            if (user == null || user.Password != passwordHash)
            {
                return null;
            }

            return GenerateToken(user);
        }

        public async Task<string> Register(RegisterDto dto)
        {
            var user = _mapper.Map<User>(dto);

            user.Password = CryptoHelper.GetMd5Hash(dto.Password);

            var role = await _rolesRepository.Get(dto.RoleId);
            user.Roles.Add(role);

            await _usersRepository.Add(user);
            await _unitOfWork.SaveChanges();

            return GenerateToken(user);
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration[AppConstants.Secret]);

            var claims = new List<Claim> { new(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            claims.AddRange(user.Roles.Select(e => new Claim(ClaimTypes.Role, e.Value.ToString())));

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
    }
}
