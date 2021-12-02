using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Reporting.BBL.Interfaces;
using Reporting.Common.Dtos;
using Reporting.Domain.Entities;
using Reporting.Domain.Interfaces;

namespace Reporting.BBL.Services
{
    public class ConferencesService : IConferencesService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISimpleRepository _repository;
        private readonly IConferencesRepository _conferencesRepository;
        private readonly IMapper _mapper;

        public ConferencesService(ICurrentUserService currentUserService,
            IUnitOfWork unitOfWork,
            ISimpleRepository repository,
            IConferencesRepository conferencesRepository,
            IMapper mapper)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
            _repository = repository;
            _conferencesRepository = conferencesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ComboboxItemDto>> GetConferenceTypes()
        {
            var types = await _repository.GetAll<ConferenceType>();

            var dtos = _mapper.Map<IEnumerable<ComboboxItemDto>>(types);

            return dtos;
        }

        public async Task<IEnumerable<ComboboxItemDto>> GetConferenceSubTypes()
        {
            var types = await _repository.GetAll<ConferenceSubType>();

            var dtos = _mapper.Map<IEnumerable<ComboboxItemDto>>(types);

            return dtos;
        }

        public async Task<IEnumerable<ConferenceDto>> GetConferences(int? typeValue, int? subTypeValue)
        {
            var userId = int.Parse(_currentUserService.UserId);
            var user = await _repository.Get<User>(userId);

            var conferences =
                await _conferencesRepository.GetDepartmentConferences(user.DepartmentId, typeValue, subTypeValue);

            var dtos = _mapper.Map<IEnumerable<ConferenceDto>>(conferences);

            return dtos;
        }

        public async Task<ConferenceDto> CreateConference(CreateConferenceDto dto)
        {
            var userId = int.Parse(_currentUserService.UserId);
            var user = await _repository.Get<User>(userId);

            var conference = _mapper.Map<CreateConferenceDto, Conference>(dto);
            conference.DepartmentId = user.DepartmentId;

            await _repository.Add(conference);
            await _unitOfWork.SaveChanges();

            conference = await _repository.Get<Conference>(conference.Id);

            return _mapper.Map<Conference, ConferenceDto>(conference);
        }

        public async Task<ConferenceDto> UpdateConference(int id, CreateConferenceDto dto)
        {
            var conference = await _repository.Get<Conference>(id);

            if (conference == null)
            {
                return null;
            }

            _mapper.Map(dto, conference);

            await _unitOfWork.SaveChanges();

            conference = await _repository.Get<Conference>(conference.Id);

            return _mapper.Map<Conference, ConferenceDto>(conference);
        }

        public async Task DeleteConference(int id)
        {
            await _repository.Remove<Conference>(id);
            await _unitOfWork.SaveChanges();
        }
    }
}
