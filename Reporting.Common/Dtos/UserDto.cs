using System.Collections.Generic;

namespace Reporting.Common.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IeeeXploreAuthorName { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public IEnumerable<int> Roles { get; set; }
        public int RoleId { get; set; }
    }
}
