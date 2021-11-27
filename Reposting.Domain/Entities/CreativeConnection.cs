namespace Reporting.Domain.Entities
{
    public class CreativeConnection : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public int TypeId { get; set; }
        public CreativeConnectionType Type { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
