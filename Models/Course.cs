using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Course
    {
        [Key]
        public int CrsId { get; set; }
        [Required]
        public string CrsName { get; set; }
        public int Duration { get; set; }

        public virtual ICollection<Department>? Departments { get; set; } = new HashSet<Department>();
        public virtual ICollection<StudentCourse>? courseStudents { get; set; } = new HashSet<StudentCourse>();
    }
}
