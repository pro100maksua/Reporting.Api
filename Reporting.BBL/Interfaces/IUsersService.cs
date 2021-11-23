using System.Collections.Generic;
using System.Threading.Tasks;
using Reporting.Common.Dtos;

namespace Reporting.BBL.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<ComboboxItemDto>> GetRoles();
        Task<IEnumerable<ComboboxItemDto>> GetFaculties();
        Task<IEnumerable<DepartmentDto>> GetDepartments(int facultyValue);
        Task UpdateUserIeeeXploreAuthorName(int userId, string name);
    }
}
