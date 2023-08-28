using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class ITIASPContext:IdentityDbContext<IdentityUser>//, DbContext,
    {
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<StudentCourse> StudentCourses { get; set; }
        public ITIASPContext()
        {
            
        }
        public ITIASPContext(DbContextOptions<ITIASPContext> options):base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString = "server=.;database=ITIASP3;Integrated Security=true;encrypt=false;";
            optionsBuilder.UseSqlServer(connString);
            //optionsBuilder.UseLazyLoadingProxies();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>().HasKey(a => new { a.StudentId, a.CourseId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
