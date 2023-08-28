using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
namespace WebApplication1.Services
{
    public class StudentService: IService.IStudentService, IService.IStudentDepartment
    {
        ITIASPContext ITIASPctx;

        public async Task<List<Student>> GetStudentByDeptId(int deptid)
        {
            return await ITIASPctx.Students.Where(d => d.DeptId == deptid).ToListAsync();
        }

        public StudentService(ITIASPContext _ITIASPctx) 
        {
            ITIASPctx = _ITIASPctx;
        }

        public async Task<List<Student>> GetAll()
        {
            return await ITIASPctx.Students.Include(a=>a.Department).ToListAsync();
        }

        public async Task<Student> GetById(int id)
        {
            Student std = await ITIASPctx.Students.Include(a=>a.Department).FirstOrDefaultAsync(a => a.Id == id);
            //ITIASPctx.Students.Include(b => b.Department);
            return std;
        }

        public async Task<Student> Add(Student student)
        {
            await ITIASPctx.Students.AddAsync(student);
            await ITIASPctx.SaveChangesAsync();

            return student;
        }

        public async Task Update(Student student)
        {
            Student stdOld = await GetById(student.Id);
            if(stdOld != null)
            {
                stdOld.FName = student.FName;
                stdOld.LName = student.LName;
                stdOld.DeptId = student.DeptId;
            }
            await ITIASPctx.SaveChangesAsync();
        }
        public async Task DeleteById(int id)
        {
            ITIASPctx.Students.Remove(await GetById(id));
            await ITIASPctx.SaveChangesAsync();
        }
    }
}
