using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reporting.DAL.EF;
using Reporting.Domain.Entities;
using Reporting.Domain.Interfaces;

namespace Reporting.DAL.Repositories
{
    public class PublicationsRepository : Repository<Publication>, IPublicationsRepository
    {
        public PublicationsRepository(ReportingDbContext context)
            : base(context)
        { }

        public async Task<IEnumerable<Publication>> GetUserPublications(int userId, int? publicationYear = default)
        {
            var publications = await DbSet.AsNoTracking()
                .Include(e => e.Type)
                .Include(e => e.Authors)
                .Where(e => e.Authors.Any(a => a.Id == userId))
                .Where(e => publicationYear == default || e.PublicationYear == publicationYear)
                .OrderByDescending(c => c.PublicationYear)
                .ThenBy(c => c.Title)
                .ToListAsync();

            return publications;
        }

        public async Task<IEnumerable<Publication>> GetDepartmentPublications(int departmentId, int? publicationYear = default)
        {
            var publications = await DbSet.AsNoTracking()
                .Include(e => e.Type)
                .Include(e => e.Authors)
                .Where(e => e.Authors.Any(a => a.DepartmentId == departmentId))
                .Where(e => publicationYear == default || e.PublicationYear == publicationYear)
                .OrderByDescending(c => c.PublicationYear)
                .ThenBy(c => c.Title)
                .ToListAsync();

            return publications;
        }
    }
}
