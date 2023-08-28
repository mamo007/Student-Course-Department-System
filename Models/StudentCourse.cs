using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int? Degree { get; set; }

        [ForeignKey("StudentId")]
        public virtual Student? Student { get; set; }
        [ForeignKey("CourseId")]
        public virtual Course? Course { get; set; }
    }
}
