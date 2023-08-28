using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using WebApplication1.IService;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class DepartmentController : Controller
    {
        //Department Service
        IDepartmentService deptsvc;
        IDepartmentCoursesService deptscoursesvc;
        IStudentDepartment IStdDeptSvc;
        IStudentCourse IStdCourseSvc;

        public DepartmentController(IDepartmentService _db, IDepartmentCoursesService _deptCrsSvc, IStudentDepartment _IStdDeptSvc, IStudentCourse _IStdCourseSvc)
        {
            deptsvc = _db;
            deptscoursesvc = _deptCrsSvc;
            IStdDeptSvc = _IStdDeptSvc;
            IStdCourseSvc = _IStdCourseSvc;
        }
        [Authorize(Roles = "Admin,Instructor,Student")]
        public async Task<IActionResult> Index()
        {
            return View(await deptsvc.GetAll());
        }
        [Authorize(Roles = "Admin,Instructor,Student")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest("Error");

            Department dept = await deptsvc.GetById(id.Value);

            if (dept == null)
                return NotFound();

            return View(dept);
        }
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Instructor")]
        [HttpPost]
        public async Task<IActionResult> Create(Department dept)
        {
            //ModelState.Remove("Students");
            if (ModelState.IsValid)
            {
                await deptsvc.Add(dept);

                return RedirectToAction("Index", "Department");
            }

            return View();
        }
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest("Error");

            Department dept = await deptsvc.GetById(id.Value);

            if (dept == null)
                return NotFound();

            return View(dept);
        }
        [Authorize(Roles = "Admin,Instructor")]
        [HttpPost]
        public async Task<IActionResult> Edit(Department dept)
        {
            if (ModelState.IsValid)
            {
                await deptsvc.Update(dept);

                return RedirectToAction("Index", "Department");
            }

            return View();
        }
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Delete(int id)
        {
            await deptsvc.DeleteById(id);

            return RedirectToAction("Index", "Department");
        }
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> updateCourses(int id)
        {
            var coursesInDept = (await deptscoursesvc.GetDepartmentWithCourses(id)).Courses;
            var coursesNotInDept = await deptscoursesvc.GetCoursesNotInDepartment(id);
            ViewBag.coursesInDept = new SelectList(coursesInDept,"CrsId", "CrsName");
            ViewBag.coursesNotInDept = new SelectList(coursesNotInDept, "CrsId", "CrsName");

            return View();
        }
        [Authorize(Roles = "Admin,Instructor")]
        [HttpPost]
        public async Task<IActionResult> updateCourses(int id, int[] CoursesToRemove, int[] CoursesToAdd)
        {
            await deptscoursesvc.RemoveCoursesFromDepartment(id, CoursesToRemove);
            await deptscoursesvc.AddCoursesToDepartment(id, CoursesToAdd);
            

            return RedirectToAction("updateCourses", new { id=id});
        }
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> ShowCourses(int id)
        {
            var Dept = (await deptscoursesvc.GetDepartmentWithCourses(id));


            return View(Dept);
        }
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> UpdateStudentDegree(int deptid, int crsid)
        {
            List<Student> students = await IStdDeptSvc.GetStudentByDeptId(deptid);
            ViewBag.deptid = deptid;
            ViewBag.crsid = crsid;
            List<StudentCourse> StdCrs = await IStdCourseSvc.GetStudentDegree(crsid);

            return View(students);
        }
        [Authorize(Roles = "Admin,Instructor")]
        [HttpPost]
        public async Task<IActionResult> UpdateStudentDegree(int deptid, int crsid, Dictionary<int,int> degree)
        {
            await IStdCourseSvc.UpdateStudentDegree(degree,crsid);

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> ShowAllDegrees(int deptid, int crsid)
        {
            List<Student> students = await IStdDeptSvc.GetStudentByDeptId(deptid);
            ViewBag.deptid = deptid;
            ViewBag.crsid = crsid;
            List<StudentCourse> StdCrs =  await IStdCourseSvc.GetStudentDegree(crsid);

            return View(students);
        }
    }
}
