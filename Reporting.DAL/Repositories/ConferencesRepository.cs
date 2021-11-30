using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reporting.DAL.EF;
using Reporting.Domain.Entities;
using Reporting.Domain.Interfaces;

namespace Reporting.DAL.Repositories
{
    public class ConferencesRepository : Repository<Conference>, IConferencesRepository
    {
        public ConferencesRepository(ReportingDbContext context)
            : base(context) { }

        public async Task<IEnumerable<Conference>> GetDepartmentConferences(int departmentId, int? year = default)
        {
            var conferences = await DbSet.AsNoTrackingWithIdentityResolution()
                .Include(e => e.Publications)
                .ThenInclude(e => e.Authors)
                .Where(e => e.Publications.Any(p => p.Authors.Any(a => a.DepartmentId == departmentId)))
                .Where(e => year == default || e.Year == year)
                .AsSplitQuery()
                .ToListAsync();

            return conferences;
        }
    }
}
