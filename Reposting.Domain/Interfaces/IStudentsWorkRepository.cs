using System.Collections.Generic;
using System.Threading.Tasks;
using Reporting.Domain.Entities;

namespace Reporting.Domain.Interfaces
{
    public interface IStudentsWorkRepository : IRepository<StudentsWorkEntry>
    {
        Task<IEnumerable<StudentsWorkEntry>> GetStudentsWorkEntries(int departmentId, int? year = default);
    }
}
