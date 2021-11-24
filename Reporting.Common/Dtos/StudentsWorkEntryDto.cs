namespace Reporting.Common.Dtos
{
    public class StudentsWorkEntryDto
    {
        public int Id { get; set; }
        public string EntryName { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Specialty { get; set; }
        public int? Place { get; set; }
        public string ScientificWorkName { get; set; }
        public bool Independently { get; set; }
        public int Year { get; set; }

        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int TypeValue { get; set; }

        public int? ScientificWorkTypeId { get; set; }
        public string ScientificWorkTypeName { get; set; }
        public int? ScientificWorkTypeValue { get; set; }
    }
}
