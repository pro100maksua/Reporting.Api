using System.Threading.Tasks;
using Reporting.DAL.EF;
using Reporting.Domain.Interfaces;

namespace Reporting.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReportingDbContext _dbContext;

        public UnitOfWork(ReportingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task SaveChanges()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
