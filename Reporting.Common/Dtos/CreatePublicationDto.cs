namespace Reporting.Common.Dtos
{
    public class CreatePublicationDto
    {
        public int TypeId { get; set; }
        public string Title { get; set; }
        public string PublicationNumber { get; set; }
        public string PublicationTitle { get; set; }
        public int PublicationYear { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public double PrintedPagesCount { get; set; }
        public string ScopusAuthors { get; set; }
        public string Doi { get; set; }
        public string Publisher { get; set; }
        public string Isbn { get; set; }
        public string ContentType { get; set; }
        public string Abstract { get; set; }
        public string ArticleNumber { get; set; }
        public string PdfUrl { get; set; }
        public string HtmlUrl { get; set; }
        public string ConferenceLocation { get; set; }
        public int? CitingPaperCount { get; set; }
        public int? CitingPatentCount { get; set; }
    }
}
