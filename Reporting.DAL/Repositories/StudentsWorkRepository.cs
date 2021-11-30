using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reporting.DAL.EF;
using Reporting.Domain.Entities;
using Reporting.Domain.Interfaces;

namespace Reporting.DAL.Repositories
{
    public class StudentsWorkRepository : Repository<StudentsWorkEntry>, IStudentsWorkRepository
    {
        public StudentsWorkRepository(ReportingDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<StudentsWorkEntry>> GetStudentsWorkEntries(int departmentId, int? year = default)
        {
            var entries = await DbSet.AsNoTracking()
                .Include(e => e.Type)
                .Include(e => e.ScientificWorkType)
                .Include(e => e.Teacher)
                .Where(e => e.Teacher.DepartmentId == departmentId)
                .Where(e => year == default || e.Created.Year == year)
                .OrderByDescending(c => c.Created.Year)
                .ThenBy(c => c.Type.Value)
                .ToListAsync();

            return entries;
        }
    }
}
