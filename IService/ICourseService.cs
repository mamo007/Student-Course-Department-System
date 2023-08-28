using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;

namespace WebApplication1.IService
{
    public interface ICourseService
    {
        public Task<List<Course>> GetAll();

        public Task<Course> GetById(int id);

        public Task<Course> Add(Course crs);

        public Task Update(Course crs);

        public Task DeleteById(int id);
    }
}
