using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reporting.DAL.EF;
using Reporting.Domain.Entities;
using Reporting.Domain.Interfaces;

namespace Reporting.DAL.Repositories
{
    public class ActivityIndicatorsRepository : Repository<ActivityIndicator>, IActivityIndicatorsRepository
    {
        public ActivityIndicatorsRepository(ReportingDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<ActivityIndicator>> GetDepartmentActivityIndicators(int departmentId)
        {
            var activityIndicators = await DbSet.AsNoTracking()
                .Where(e => e.DepartmentId == departmentId)
                .OrderByDescending(c => c.Year)
                .ToListAsync();

            return activityIndicators;
        }
    }
}
