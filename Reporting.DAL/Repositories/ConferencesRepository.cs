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
            : base(context)
        {
        }

        public async Task<IEnumerable<Conference>> GetDepartmentConferences(int departmentId,
            int? typeValue = default,
            int? subTypeValue = default,
            int? year = default)
        {
            var conferences = await DbSet.AsNoTrackingWithIdentityResolution()
                .Include(e => e.Publications)
                .ThenInclude(e => e.Authors)
                .Where(e => e.DepartmentId == departmentId)
                .Where(e => typeValue == default || e.Type.Value == typeValue)
                .Where(e => subTypeValue == default || e.SubType.Value == subTypeValue)
                .Where(e => year == default || e.StartDate.Value.Year == year)
                .OrderByDescending(e => e.StartDate.Value.Year)
                .ThenBy(c => c.Title)
                .AsSplitQuery()
                .ToListAsync();

            return conferences;
        }
    }
}
