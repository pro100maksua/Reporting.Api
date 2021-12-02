using System;

namespace Reporting.Common.Dtos
{
    public class ConferenceDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Number { get; set; }
        public string Location { get; set; }
        public string Organizers { get; set; }
        public string CoOrganizers { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DateRange { get; set; }
        public int? NumberOfParticipants { get; set; }

        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int TypeValue { get; set; }

        public int? SubTypeId { get; set; }
        public string SubTypeName { get; set; }
        public int? SubTypeValue { get; set; }
    }
}
