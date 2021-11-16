using System.Collections;
using System.Collections.Generic;

namespace Reporting.Domain.Entities
{
    public class Role : ComboboxItem
    {
        public Role()
        {
            Users = new List<User>();
        }

        public int Id { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
