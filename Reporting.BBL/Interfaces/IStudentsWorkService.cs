using System.Collections.Generic;
using System.Threading.Tasks;
using Reporting.Common.Dtos;

namespace Reporting.BBL.Interfaces
{
    public interface IStudentsWorkService
    {
        Task<IEnumerable<ComboboxItemDto>> GetStudentsWorkTypes();
        Task<IEnumerable<ComboboxItemDto>> GetStudentsScientificWorkTypes();
        Task<IEnumerable<StudentsWorkEntryDto>> GetStudentsWorkEntries();
        Task CreateStudentsWorkEntry(CreateStudentsWorkEntryDto dto);
        Task UpdateStudentsWorkEntry(int id, CreateStudentsWorkEntryDto dto);
        Task DeleteStudentsWorkEntry(int id);
    }
}
