using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Reporting.BBL.Interfaces;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;
using Reporting.Domain.Interfaces;

namespace Reporting.BBL.Services
{
    public class ActivityIndicatorsService : IActivityIndicatorsService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActivityIndicatorsRepository _activityIndicatorsRepository;
        private readonly ISimpleRepository _repository;
        private readonly IMapper _mapper;

        public ActivityIndicatorsService(ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork,
            IActivityIndicatorsRepository activityIndicatorsRepository,
            ISimpleRepository repository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _activityIndicatorsRepository = activityIndicatorsRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ActivityIndicatorDto>> GetDepartmentActivityIndicators()
        {
            var userId = int.Parse(_currentUserService.UserId);
            var user = await _repository.Get<User>(userId);

            var entries = await _activityIndicatorsRepository.GetDepartmentActivityIndicators(user.DepartmentId);

            var dtos = _mapper.Map<IEnumerable<ActivityIndicatorDto>>(entries);

            return dtos;
        }

        public async Task CreateActivityIndicator(CreateActivityIndicatorDto dto)
        {
            var userId = int.Parse(_currentUserService.UserId);
            var user = await _repository.Get<User>(userId);

            var entry = _mapper.Map<CreateActivityIndicatorDto, ActivityIndicator>(dto);
            entry.DepartmentId = user.DepartmentId;

            await _activityIndicatorsRepository.Add(entry);
            await _unitOfWork.SaveChanges();
        }

        public async Task UpdateActivityIndicator(int id, CreateActivityIndicatorDto dto)
        {
            var entry = await _activityIndicatorsRepository.Get(id);

            if (entry == null)
            {
                return;
            }

            _mapper.Map(dto, entry);

            await _unitOfWork.SaveChanges();
        }

        public async Task DeleteActivityIndicator(int id)
        {
            await _activityIndicatorsRepository.Remove(id);
            await _unitOfWork.SaveChanges();
        }
    }
}
