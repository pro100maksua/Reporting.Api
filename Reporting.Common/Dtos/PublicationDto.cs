using System.Collections.Generic;

namespace Reporting.Common.Dtos
{
    public class PublicationDto
    {
        public string Title { get; set; }
        public string PublicationTitle { get; set; }
        public int PublicationYear { get; set; }
        public string StartPage { get; set; }
        public string EndPage { get; set; }
        public int PagesCount { get; set; }
        public double? PrintedPagesCount { get; set; }

        public IEnumerable<PublicationAuthorDto> Authors { get; set; }
    }
}
