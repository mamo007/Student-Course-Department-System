using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Student
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required,StringLength(16,MinimumLength =3)]
        public string FName { get; set; }
        [Required, StringLength(16, MinimumLength = 3)]
        public string LName { get; set; }
        [Range(21,30)]
        public int Age { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public int DeptId { get; set; }

        [ForeignKey("DeptId")]
        public virtual Department? Department { get; set; }
        public virtual ICollection<StudentCourse>? StudentCourses { get; set; } = new HashSet<StudentCourse>();

        public override string ToString()
        {
            return $"{Id}:{FName} {LName}:{Department.Name}";
        }
    }
}
