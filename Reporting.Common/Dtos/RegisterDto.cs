namespace Reporting.Common.Dtos
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Degree { get; set; }
        public string AcademicStatus { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int DepartmentId { get; set; }
        public int RoleId { get; set; }
        public string IeeeXploreAuthorName { get; set; }
    }
}
