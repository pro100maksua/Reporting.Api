using System.Threading.Tasks;

namespace Reporting.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task SaveChanges();   
    }
}
