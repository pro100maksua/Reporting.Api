using System.Collections.Generic;
using System.Threading.Tasks;
using Reporting.Domain.Entities;

namespace Reporting.Domain.Interfaces
{
    public interface ICreativeConnectionsRepository : IRepository<CreativeConnection>
    {
        Task<IEnumerable<CreativeConnection>> GetDepartmentCreativeConnections(int departmentId, int? year = default);
    }
}
