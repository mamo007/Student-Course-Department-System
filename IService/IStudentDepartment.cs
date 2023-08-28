using WebApplication1.Models;

namespace WebApplication1.IService
{
    public interface IStudentDepartment
    {
        public Task<List<Student>> GetStudentByDeptId(int deptid);
    }
}
