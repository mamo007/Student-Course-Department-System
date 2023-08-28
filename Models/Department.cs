using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Department
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, StringLength(16, MinimumLength = 3)]
        public string Name { get; set; }
        public int Capacity { get; set; }

        public virtual ICollection<Student>? Students { get; set; } = new HashSet<Student>();
        public virtual ICollection<Course>? Courses { get; set; } = new HashSet<Course>();
    }
}
