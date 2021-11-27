using System.Collections.Generic;
using System.Threading.Tasks;
using Reporting.Common.Dtos;

namespace Reporting.BBL.Interfaces
{
    public interface ICreativeConnectionsService
    {
        Task<IEnumerable<ComboboxItemDto>> GetCreativeConnectionTypes();
        Task<IEnumerable<CreativeConnectionDto>> GetDepartmentCreativeConnections();
        Task CreateCreativeConnection(CreateCreativeConnectionDto dto);
        Task UpdateCreativeConnection(int id, CreateCreativeConnectionDto dto);
        Task DeleteCreativeConnection(int id);
    }
}
