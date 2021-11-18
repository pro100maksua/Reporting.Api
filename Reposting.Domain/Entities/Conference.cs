using System.Collections.Generic;

namespace Reporting.Domain.Entities
{
    public class Conference : AuditableEntity
    {
        public Conference()
        {
            Publications = new List<Publication>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public string Location { get; set; }
        public int Year { get; set; }

        public ICollection<Publication> Publications { get; set; }
    }
}
