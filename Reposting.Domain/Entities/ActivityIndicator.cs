namespace Reporting.Domain.Entities
{
    public class ActivityIndicator : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Year { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
