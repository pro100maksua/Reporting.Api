using System.Collections.Generic;
using System.Threading.Tasks;
using Reporting.Common.Dtos;

namespace Reporting.BBL.Interfaces
{
    public interface IConferencesService
    {
        Task<IEnumerable<ComboboxItemDto>> GetConferenceTypes();
        Task<IEnumerable<ComboboxItemDto>> GetConferenceSubTypes();
        Task<IEnumerable<ConferenceDto>> GetConferences(int? typeValue, int? subTypeValue);
        Task<ConferenceDto> CreateConference(CreateConferenceDto dto);
        Task<ConferenceDto> UpdateConference(int id, CreateConferenceDto dto);
        Task DeleteConference(int id);
    }
}
