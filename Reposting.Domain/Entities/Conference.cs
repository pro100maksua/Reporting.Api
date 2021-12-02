using System;
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
        public string Organizers { get; set; }
        public string CoOrganizers { get; set; }
        public string Number { get; set; }
        public string Location { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? NumberOfParticipants { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int TypeId { get; set; }
        public ConferenceType Type { get; set; }
        public int? SubTypeId { get; set; }
        public ConferenceSubType SubType { get; set; }

        public ICollection<Publication> Publications { get; set; }
    }
}
