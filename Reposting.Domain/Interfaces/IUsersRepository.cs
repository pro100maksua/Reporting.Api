using System.Collections.Generic;
using System.Threading.Tasks;
using Reporting.Domain.Entities;

namespace Reporting.Domain.Interfaces
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetDepartmentUsersWithPublications(int departmentId, int publicationYear);
    }
}
