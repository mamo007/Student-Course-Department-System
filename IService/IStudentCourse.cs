using WebApplication1.Models;

namespace WebApplication1.IService
{
    public interface IStudentCourse
    {
        public Task UpdateStudentDegree(Dictionary<int, int> stdDegree, int crsId);
        public Task<List<StudentCourse>> GetStudentDegree(int crsId);
    }
}
