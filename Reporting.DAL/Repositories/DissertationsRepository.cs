using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reporting.DAL.EF;
using Reporting.Domain.Entities;
using Reporting.Domain.Interfaces;

namespace Reporting.DAL.Repositories
{
    public class DissertationsRepository : Repository<Dissertation>, IDissertationsRepository
    {
        public DissertationsRepository(ReportingDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Dissertation>> GetUserDissertations(int userId)
        {
            var publications = await DbSet.AsNoTracking()
                .Include(e => e.Author)
                .Where(e => e.Author.Id == userId)
                .OrderByDescending(e => e.DefenseDate)
                .ToListAsync();

            return publications;
        }

        public async Task<IEnumerable<Dissertation>> GetDepartmentDissertations(int departmentId, int? year = default)
        {
            var connections = await DbSet.AsNoTracking()
                .Include(e => e.Author)
                .Where(e => e.DepartmentId == departmentId)
                .Where(e => year == default || e.DefenseDate.Year == year)
                .OrderByDescending(e => e.DefenseDate)
                .ToListAsync();

            return connections;
        }
    }
}
