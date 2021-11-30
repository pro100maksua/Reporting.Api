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

        public async Task<ActivityIndicatorDto> GetDepartmentActivityIndicator(int year)
        {
            var userId = int.Parse(_currentUserService.UserId);
            var user = await _repository.Get<User>(userId);

            var activityIndicator = await _activityIndicatorsRepository.Get(e =>
                e.DepartmentId == user.DepartmentId && e.Year == year);

            var dtos = _mapper.Map<ActivityIndicatorDto>(activityIndicator);

            return dtos;
        }

        public async Task CreateActivityIndicator(CreateActivityIndicatorDto dto)
        {
            var userId = int.Parse(_currentUserService.UserId);
            var user = await _repository.Get<User>(userId);

            var activityIndicator = await _activityIndicatorsRepository.Get(e =>
                e.DepartmentId == user.DepartmentId && e.Year == dto.Year);

            if (activityIndicator != null)
            {
                UpdateActivityIndicator(activityIndicator, dto);
            }
            else
            {
                activityIndicator = _mapper.Map<CreateActivityIndicatorDto, ActivityIndicator>(dto);
                activityIndicator.DepartmentId = user.DepartmentId;

                await _activityIndicatorsRepository.Add(activityIndicator);
            }

            await _unitOfWork.SaveChanges();
        }

        public async Task UpdateActivityIndicator(int id, CreateActivityIndicatorDto dto)
        {
            var activityIndicator = await _activityIndicatorsRepository.Get(id);

            if (activityIndicator == null)
            {
                return;
            }

            UpdateActivityIndicator(activityIndicator, dto);

            await _unitOfWork.SaveChanges();
        }

        public async Task DeleteActivityIndicator(int id)
        {
            await _activityIndicatorsRepository.Remove(id);
            await _unitOfWork.SaveChanges();
        }

        private void UpdateActivityIndicator(ActivityIndicator activityIndicator, CreateActivityIndicatorDto dto)
        {
            _mapper.Map(dto, activityIndicator);
        }
    }
}
