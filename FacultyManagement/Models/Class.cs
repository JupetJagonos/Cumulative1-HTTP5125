namespace SchoolManagementSystem.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public int? TeacherId { get; set; }
    }
}
