namespace Reporting.Domain.Entities
{
    public class StudentsWorkEntry : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Specialty { get; set; }
        public int? Place { get; set; }
        public string ScientificWorkName { get; set; }
        public bool Independently { get; set; }

        public int TypeId { get; set; }
        public StudentsWorkType Type { get; set; }

        public int? ScientificWorkTypeId { get; set; }
        public StudentsScientificWorkType ScientificWorkType { get; set; }

        public int TeacherId { get; set; }
        public User Teacher { get; set; }
    }
}
