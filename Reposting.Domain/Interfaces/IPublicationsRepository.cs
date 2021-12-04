using System.Collections.Generic;
using System.Threading.Tasks;
using Reporting.Domain.Entities;

namespace Reporting.Domain.Interfaces
{
    public interface IPublicationsRepository : IRepository<Publication>
    {
        Task<IEnumerable<Publication>> GetUserPublications(int userId, int? publicationYear = default);
        Task<IEnumerable<Publication>> GetDepartmentPublications(int? departmentId, int? publicationYear = default);
        Task<IEnumerable<Publication>> GetDepartmentForeignPublications(int departmentId, int? publicationYear = default);
    }
}
