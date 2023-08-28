using WebApplication1.Models;

namespace WebApplication1.IService
{
    public interface IDepartmentCoursesService
    {
        public Task<Department> GetDepartmentWithCourses(int DeptId);
        public Task<List<Course>> GetCoursesNotInDepartment(int DeptId);
        public Task AddCoursesToDepartment(int deptid, int[] courseIds);
        public Task RemoveCoursesFromDepartment(int deptid, int[] courseIds);
    }
}
