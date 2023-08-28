using WebApplication1.Models;

namespace WebApplication1.IService
{
    public interface IStudentService
    {
        public Task<List<Student>> GetAll();
        public Task<Student> GetById(int id);
        public Task<Student> Add(Student student);
        public Task Update(Student student);
        public Task DeleteById(int id);
    }
}
