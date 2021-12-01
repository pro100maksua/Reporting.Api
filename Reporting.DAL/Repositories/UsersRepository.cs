using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reporting.DAL.EF;
using Reporting.Domain.Entities;
using Reporting.Domain.Interfaces;

namespace Reporting.DAL.Repositories
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(ReportingDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetDepartmentUsersWithPublications(int departmentId, int publicationYear)
        {
            var users = await DbSet.AsNoTracking()
                .Include(e => e.Publications.Where(p => p.PublicationYear == publicationYear))
                .Where(e => e.DepartmentId == departmentId)
                .ToListAsync();

            return users;
        }
    }
}
