using Microsoft.EntityFrameworkCore;
using WebApplication1.IService;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class DepartmentCoursesService:IDepartmentCoursesService
    {
        ITIASPContext ctx;
        IDepartmentService deptsvc;
        ICourseService crssvc;
        public DepartmentCoursesService(ITIASPContext _ctx, IDepartmentService _deptsvc, ICourseService _crssvc)
        {
            ctx = _ctx;
            deptsvc = _deptsvc;
            crssvc = _crssvc;
        }

        public async Task<Department> GetDepartmentWithCourses(int DeptId)
        {
            return await ctx.Departments.Include(a=>a.Courses).FirstOrDefaultAsync(a=>a.Id == DeptId);
        }
        public async Task<List<Course>> GetCoursesNotInDepartment(int DeptId)
        {
            var dept = await GetDepartmentWithCourses(DeptId);
            var allCourses = await ctx.Courses.ToListAsync();
            return allCourses.Except(dept.Courses).ToList();
        }
        public async Task AddCoursesToDepartment(int deptid, int[] courseIds)
        {
            Department dept = await deptsvc.GetById(deptid);
            foreach(var crsid in courseIds)
            {
                var crs = await crssvc.GetById(crsid);
                //if(dept.Courses.FirstOrDefault(a=>a.CrsId == crsid) == null)
                    dept.Courses.Add(crs);
            }
            await ctx.SaveChangesAsync();
        }
        public async Task RemoveCoursesFromDepartment(int deptid, int[] courseIds)
        {
            Department dept = await deptsvc.GetById(deptid);
            foreach (var crsid in courseIds)
            {
                var crs = await crssvc.GetById(crsid);
                //if(dept.Courses.FirstOrDefault(a=>a.CrsId == crsid) == null)
                dept.Courses.Remove(crs);
            }
            await ctx.SaveChangesAsync();
        }
    }
}
