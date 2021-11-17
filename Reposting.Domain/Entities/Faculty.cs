using System.Collections.Generic;

namespace Reporting.Domain.Entities
{
    public class Faculty : ComboboxItem
    {
        public Faculty()
        {
            Departments = new List<Department>();
        }

        public int Id { get; set; }

        public ICollection<Department> Departments { get; set; }
    }
}
