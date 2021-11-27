namespace Reporting.Common.Dtos
{
    public class CreativeConnectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int TypeValue { get; set; }
    }
}
