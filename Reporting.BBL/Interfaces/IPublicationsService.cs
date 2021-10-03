using System.Collections.Generic;
using System.Threading.Tasks;
using Reporting.Common.Dtos;

namespace Reporting.BBL.Interfaces
{
    public interface IPublicationsService
    {
        Task<IEnumerable<ComboboxItemDto>> GetPublicationTypes();
        Task<PublicationDto> CreatePublication(CreatePublicationDto dto);
        Task<PublicationDto> GetPublicationFromScopus(string articleNumber, string title);
    }
}
