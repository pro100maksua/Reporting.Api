using System.Collections.Generic;
using System.Threading.Tasks;
using Reporting.Common.Dtos;

namespace Reporting.BBL.Interfaces
{
    public interface IPublicationsService
    {
        Task<IEnumerable<ComboboxItemDto>> GetPublicationTypes();
        Task<IEnumerable<PublicationDto>> GetUserPublications(int userId);
        Task<PublicationDto> CreatePublication(CreatePublicationDto dto);
        Task<PublicationDto> UpdatePublication(int id, CreatePublicationDto dto);
        Task DeletePublication(int id);
        Task<PublicationDto> GetPublicationFromIeeeXplore(string articleNumber, string title);
        Task LoadScientificJournalsCategoryB();
    }
}
