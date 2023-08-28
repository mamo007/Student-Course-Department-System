using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.IService;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class CourseService : ICourseService
    {
        ITIASPContext ITIASPctx;
        public CourseService(ITIASPContext _ITIASPctx)
        {
            ITIASPctx = _ITIASPctx;
        }

        public async Task<Course> Add(Course crs)
        {
            await ITIASPctx.AddAsync(crs);
            await ITIASPctx.SaveChangesAsync();

            return crs;
        }

        public async Task DeleteById(int id)
        {
            ITIASPctx.Courses.Remove(await GetById(id));
            await ITIASPctx.SaveChangesAsync();
        }

        public async Task<List<Course>> GetAll()
        {
            return await ITIASPctx.Courses.Include(d=>d.Departments).ToListAsync();
        }

        public async Task<Course> GetById(int id)
        {
            return await ITIASPctx.Courses.Include(d => d.Departments).FirstOrDefaultAsync(a=>a.CrsId == id);
        }


        public async Task Update(Course crs)
        {
            Course old = await GetById(crs.CrsId);

            if (old != null)
            {
                old.CrsName = crs.CrsName;
                old.Duration = crs.Duration;
            }
            await ITIASPctx.SaveChangesAsync();
        }
    }
}
