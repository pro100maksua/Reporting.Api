using System.Collections.Generic;
using System.Threading.Tasks;
using Reporting.Domain.Entities;

namespace Reporting.Domain.Interfaces
{
    public interface IActivityIndicatorsRepository : IRepository<ActivityIndicator>
    {
        Task<IEnumerable<ActivityIndicator>> GetDepartmentActivityIndicators(int departmentId, int? year = default);
    }
}
