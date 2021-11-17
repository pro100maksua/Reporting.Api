namespace Reporting.Common.ApiModels
{
    public class ScopusArticle
    {
        public string Doi { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Isbn { get; set; }
        public string ContentType { get; set; }
        public string Abstract { get; set; }
        public string ArticleNumber { get; set; }
        public string PdfUrl { get; set; }
        public string HtmlUrl { get; set; }
        public string PublicationTitle { get; set; }
        public string ConferenceLocation { get; set; }
        public int PublicationYear { get; set; }
        public string StartPage { get; set; }
        public string EndPage { get; set; }
        public int CitingPaperCount { get; set; }
        public int CitingPatentCount { get; set; }

        public ScopusAuthors Authors { get; set; }
    }
}
