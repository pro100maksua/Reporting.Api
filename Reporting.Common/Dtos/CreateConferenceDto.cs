using System;

namespace Reporting.Common.Dtos
{
    public class CreateConferenceDto
    {
        public int TypeId { get; set; }
        public int? SubTypeId { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Organizers { get; set; }
        public string CoOrganizers { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? NumberOfParticipants { get; set; }
    }
}
