using System;

namespace Reporting.Common.Dtos
{
    public class CreateDissertationDto
    {
        public string PlaceOfWork { get; set; }
        public string Supervisor { get; set; }
        public string Specialty { get; set; }
        public string Topic { get; set; }
        public string Deadline { get; set; }
        public DateTime DefenseDate { get; set; }
        public string DefensePlace { get; set; }
        public DateTime? DiplomaReceiptDate { get; set; }
        public string AuthorName { get; set; }
    }
}
