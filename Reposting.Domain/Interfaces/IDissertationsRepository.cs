using System.Collections.Generic;
using System.Threading.Tasks;
using Reporting.Domain.Entities;

namespace Reporting.Domain.Interfaces
{
    public interface IDissertationsRepository : IRepository<Dissertation>
    {
        Task<IEnumerable<Dissertation>> GetUserDissertations(int userId);
        Task<IEnumerable<Dissertation>> GetDepartmentDissertations(int departmentId, int? year = default);
    }
}
