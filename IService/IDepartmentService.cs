using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;

namespace WebApplication1.IService
{
    public interface IDepartmentService
    {
        
        public SelectList GetNameByIdSelectList();

        public Task<List<Department>> GetAll();

        public Task<Department> GetById(int id);

        public Task<Department> Add(Department dept);

        public Task Update(Department dept);

        public Task DeleteById(int id);
    }
}
