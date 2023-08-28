using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.IService;
using WebApplication1.Models;
namespace WebApplication1.Services
{
    public class DepartmentService:IDepartmentService
    {
        ITIASPContext ITIASPctx;
        public DepartmentService(ITIASPContext _ITIASPctx)
        {
            ITIASPctx = _ITIASPctx;   
        }

        public SelectList GetNameByIdSelectList()
        {
            return new SelectList(ITIASPctx.Departments.ToList(), "Id", "Name");
        }

        public async Task<List<Department>> GetAll()
        {
            return await ITIASPctx.Departments.Include(s=>s.Students).ToListAsync();
        }

        public async Task<Department> GetById(int id)
        {
            return await ITIASPctx.Departments.Include(s => s.Students).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Department> Add (Department dept)
        {
            await ITIASPctx.Departments.AddAsync(dept);
            await ITIASPctx.SaveChangesAsync();

            return dept;
        }

        public async Task Update (Department dept)
        {
            Department old = await GetById(dept.Id);

            if (old != null)
            {
                old.Name = dept.Name;
                old.Capacity = dept.Capacity;
            }
            await ITIASPctx.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            ITIASPctx.Departments.Remove(await GetById(id));
            await ITIASPctx.SaveChangesAsync();
        }
    }
}
