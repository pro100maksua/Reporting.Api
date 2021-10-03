using System.Threading.Tasks;
using Reporting.Common.Dtos;

namespace Reporting.BBL.Interfaces
{
    public interface IPublicationsService
    {
        Task<PublicationDto> CreatePublication(CreatePublicationDto dto);
        Task<PublicationDto> GetPublicationFromScopus(string title);
    }
}
