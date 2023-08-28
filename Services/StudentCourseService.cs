using Microsoft.EntityFrameworkCore;
using WebApplication1.IService;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class StudentCourseService : IStudentCourse
    {
        ITIASPContext ITIASPctx;
        public StudentCourseService(ITIASPContext _ITIASPctx)
        {
            ITIASPctx = _ITIASPctx;
        }
        public async Task UpdateStudentDegree(Dictionary<int,int> stdDegree, int crsId)
        {
            foreach(var item in stdDegree)
            {
                var res = await ITIASPctx.StudentCourses.FirstOrDefaultAsync(a=>a.CourseId == crsId && a.StudentId == item.Key);
                if (res != null)
                    res.Degree = item.Value;
                else
                    ITIASPctx.StudentCourses.Add(new Models.StudentCourse { CourseId = crsId,StudentId = item.Key, Degree=item.Value});
            }
            await ITIASPctx.SaveChangesAsync();
        }
        public async Task<List<StudentCourse>> GetStudentDegree(int crsId)
        {
            return await ITIASPctx.StudentCourses.Where(a => a.CourseId == crsId).ToListAsync();
        }
    }
}
