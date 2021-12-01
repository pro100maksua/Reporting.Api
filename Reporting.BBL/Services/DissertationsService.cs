using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Reporting.BBL.Interfaces;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;
using Reporting.Domain.Interfaces;

namespace Reporting.BBL.Services
{
    public class DissertationsService : IDissertationsService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDissertationsRepository _dissertationsRepository;
        private readonly ISimpleRepository _repository;
        private readonly IMapper _mapper;

        public DissertationsService(ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork,
            IDissertationsRepository dissertationsRepository,
            ISimpleRepository repository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _dissertationsRepository = dissertationsRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DissertationDto>> GetUserDissertations()
        {
            var userId = int.Parse(_currentUserService.UserId);

            var dissertations = await _dissertationsRepository.GetUserDissertations(userId);

            var dtos = _mapper.Map<IEnumerable<DissertationDto>>(dissertations);

            return dtos;
        }

        public async Task<IEnumerable<DissertationDto>> GetDepartmentDissertations()
        {
            var userId = int.Parse(_currentUserService.UserId);
            var user = await _repository.Get<User>(userId);

            var dissertations = await _dissertationsRepository.GetDepartmentDissertations(user.DepartmentId);

            var dtos = _mapper.Map<IEnumerable<DissertationDto>>(dissertations);

            return dtos;
        }

        public async Task CreateDissertation(CreateDissertationDto dto)
        {
            var userId = int.Parse(_currentUserService.UserId);
            var user = await _repository.Get<User>(userId);

            var dissertation = _mapper.Map<CreateDissertationDto, Dissertation>(dto);
            dissertation.DepartmentId = user.DepartmentId;

            if (string.IsNullOrEmpty(dto.AuthorName))
            {
                dissertation.AuthorId = userId;
            }

            await _dissertationsRepository.Add(dissertation);
            await _unitOfWork.SaveChanges();
        }

        public async Task UpdateDissertation(int id, CreateDissertationDto dto)
        {
            var dissertation = await _dissertationsRepository.Get(id);

            if (dissertation == null)
            {
                return;
            }

            _mapper.Map(dto, dissertation);

            await _unitOfWork.SaveChanges();
        }

        public async Task DeleteDissertation(int id)
        {
            await _dissertationsRepository.Remove(id);
            await _unitOfWork.SaveChanges();
        }
    }
}
