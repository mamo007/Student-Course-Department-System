using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.IService;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CourseController : Controller
    {
        //Department Service
        ICourseService crssvc;

        public CourseController(ICourseService _crssvc)
        {
            crssvc = _crssvc;
        }
        [Authorize(Roles = "Admin,Instructor,Student")]
        public async Task<IActionResult> Index()
        {
            return View(await crssvc.GetAll());
        }
        [Authorize(Roles = "Admin,Instructor,Student")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest("Error");

            Course crs = await crssvc.GetById(id.Value);

            if (crs == null)
                return NotFound();

            return View(crs);
        }
        [Authorize(Roles = "Admin,Instructor")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Instructor")]
        [HttpPost]
        public async Task<IActionResult> Create(Course crs)
        {
            //ModelState.Remove("Students");
            if (ModelState.IsValid)
            {
                await crssvc.Add(crs);

                return RedirectToAction("Index", "Course");
            }

            return View();
        }
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest("Error");

            Course crs = await crssvc.GetById(id.Value);

            if (crs == null)
                return NotFound();

            return View(crs);
        }
        [Authorize(Roles = "Admin,Instructor")]
        [HttpPost]
        public async Task<IActionResult> Edit(Course crs)
        {
            if (ModelState.IsValid)
            {
                await crssvc.Update(crs);

                return RedirectToAction("Index", "Course");
            }

            return View();
        }
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Delete(int id)
        {
            await crssvc.DeleteById(id);

            return RedirectToAction("Index", "Course");
        }
    }
}
