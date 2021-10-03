namespace Reporting.Domain.Entities
{
    public class Publication : AuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PublicationTitle { get; set; }
        public int PublicationYear { get; set; }
        public int PagesCount { get; set; }
        public double PrintedPagesCount { get; set; }
        public string Authors { get; set; }
    }
}
