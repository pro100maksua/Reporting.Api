using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reporting.DAL.EF;
using Reporting.Domain.Entities;
using Reporting.Domain.Interfaces;

namespace Reporting.DAL.Repositories
{
    public class CreativeConnectionsRepository : Repository<CreativeConnection>, ICreativeConnectionsRepository
    {
        public CreativeConnectionsRepository(ReportingDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<CreativeConnection>> GetDepartmentCreativeConnections(int departmentId, int? year = default)
        {
            var connections = await DbSet.AsNoTracking()
                .Include(e => e.Type)
                .Where(e => e.DepartmentId == departmentId)
                .Where(e => year == default || e.Created.Year == year)
                .OrderByDescending(c => c.Created.Year)
                .ThenBy(c => c.Type.Value)
                .ThenBy(c => c.Name)
                .ToListAsync();

            return connections;
        }
    }
}
