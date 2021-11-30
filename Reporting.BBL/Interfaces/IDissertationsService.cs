using System.Collections.Generic;
using System.Threading.Tasks;
using Reporting.Common.Dtos;

namespace Reporting.BBL.Interfaces
{
    public interface IDissertationsService
    {
        Task<IEnumerable<DissertationDto>> GetUserDissertations();
        Task<IEnumerable<DissertationDto>> GetDepartmentDissertations();
        Task CreateDissertation(CreateDissertationDto dto);
        Task UpdateDissertation(int id, CreateDissertationDto dto);
        Task DeleteDissertation(int id);
    }
}
