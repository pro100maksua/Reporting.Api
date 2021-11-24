namespace Reporting.Common.Dtos
{
    public class CreateStudentsWorkEntryDto
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public string Specialty { get; set; }
        public int? Place { get; set; }
        public string ScientificWorkName { get; set; }
        public bool Independently { get; set; }

        public int TypeId { get; set; }
        public int? ScientificWorkTypeId { get; set; }
    }
}
