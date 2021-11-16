using System.Collections.Generic;

namespace Reporting.Domain.Entities
{
    public class User : AuditableEntity
    {
        public User()
        {
            Roles = new List<Role>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ScopusAuthorName { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}
