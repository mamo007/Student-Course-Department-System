using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using WebApplication1.IService;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        //StudentService stdsvc = new StudentService();
        //DepartmentService deptsvc = new DepartmentService();
        IStudentService stdsvc;
        IDepartmentService deptsvc;
        public StudentController(IStudentService _db, IDepartmentService _db2)
        {
            stdsvc = _db;
            deptsvc = _db2;
        }
        [Authorize(Roles = "Admin,Instructor,Student")]
        public async Task<IActionResult> Index()
        {
            ViewBag.deptname =  deptsvc.GetNameByIdSelectList();

            return View(await stdsvc.GetAll());
        }
        [Authorize(Roles = "Admin,Instructor,Student")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest("Error");

            Student student = await stdsvc.GetById(id.Value);

            if (student == null)
                return NotFound();

            return View(student);
        }
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Create()
        {
            ViewBag.deptname = deptsvc.GetNameByIdSelectList();

            return View();
        }
        [Authorize(Roles = "Admin,Instructor")]
        [HttpPost]
        public async Task<IActionResult> Create(Student Std)
        {
            //stdsvc.
            //Std.Department = deptsvc.GetById(Std.DeptId);
            //ModelState.Remove("Department");
            if (ModelState.IsValid)
            {
                await stdsvc.Add(Std);

                return RedirectToAction("Index", "Student");
            }
            ViewBag.deptname = deptsvc.GetNameByIdSelectList();

            return View();
        }
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest("Error");

            Student student = await stdsvc.GetById(id.Value);

            if(student == null)
                return NotFound();

            ViewBag.deptname = deptsvc.GetNameByIdSelectList();

            return View(student);
        }
        [Authorize(Roles = "Admin,Instructor")]
        [HttpPost]
        public async Task<IActionResult> Edit(Student std)
        {
            if (ModelState.IsValid)
            {
                await stdsvc.Update(std);

                return RedirectToAction("Index", "Student");
            }
            ViewBag.deptname = deptsvc.GetNameByIdSelectList();

            return View();
        }
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Delete(int id)
        {
            await stdsvc.DeleteById(id);

            return RedirectToAction("Index", "Student");
        }
    }
}
