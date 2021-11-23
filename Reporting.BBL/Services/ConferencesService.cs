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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Conference> _conferencesRepository;
        private readonly IMapper _mapper;

        public ConferencesService(IUnitOfWork unitOfWork, IRepository<Conference> conferencesRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _conferencesRepository = conferencesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ConferenceDto>> GetConferences()
        {
            var conferences =
                await _conferencesRepository.GetAll(orderBy: e =>
                    e.OrderByDescending(c => c.Year).ThenBy(c => c.Title));

            var dtos = _mapper.Map<IEnumerable<ConferenceDto>>(conferences);

            return dtos;
        }

        public async Task<ConferenceDto> CreateConference(CreateConferenceDto dto)
        {
            var conference = _mapper.Map<CreateConferenceDto, Conference>(dto);

            await _conferencesRepository.Add(conference);
            await _unitOfWork.SaveChanges();

            conference = await _conferencesRepository.Get(conference.Id);

            return _mapper.Map<Conference, ConferenceDto>(conference);
        }

        public async Task<ConferenceDto> UpdateConference(int id, CreateConferenceDto dto)
        {
            var conference = await _conferencesRepository.Get(id);

            if (conference == null)
            {
                return null;
            }

            _mapper.Map(dto, conference);

            await _unitOfWork.SaveChanges();

            conference = await _conferencesRepository.Get(conference.Id);

            return _mapper.Map<Conference, ConferenceDto>(conference);
        }

        public async Task DeleteConference(int id)
        {
            await _conferencesRepository.Remove(id);
            await _unitOfWork.SaveChanges();
        }
    }
}
