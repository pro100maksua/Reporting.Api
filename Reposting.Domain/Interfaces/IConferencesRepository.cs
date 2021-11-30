using System.Collections.Generic;
using System.Threading.Tasks;
using Reporting.Domain.Entities;

namespace Reporting.Domain.Interfaces
{
    public interface IConferencesRepository : IRepository<Conference>
    {
        Task<IEnumerable<Conference>> GetDepartmentConferences(int departmentId, int? year = default);
    }
}
