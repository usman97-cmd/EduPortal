using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Mangement_System.Data;
using Student_Mangement_System.Models;

namespace Student_Mangement_System.Controllers
{
    public class CourseController(AppDbContext context): Controller
    {
        public IActionResult Index()
        {
            var courses = context.Courses
                          .Include(s => s.Department)
                          .ToList();
            return View(courses);
        }
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(
               context.Departments,
               "Id",
               "Name"
               );
            return View();
        }
        [HttpPost]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                context.Courses.Add(course);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = new SelectList(
              context.Departments,
              "Id",
              "Name"
              );
            return View(course);

        }
        public IActionResult Edit(int id)
        {
            var courses = context.Courses.Find(id);
            if( courses == null)
            {
                return NotFound();
            }
            ViewBag.Department = new SelectList(
                context.Departments,
                "Id",
                "Name",
                courses.DepartmentId
                );
            return View(courses);
        }
        [HttpPost]
        public IActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                context.Courses.Update(course);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Department = new SelectList(
                context.Departments,
                "Id",
                "Name",
                course.DepartmentId
                );
            return View(course);

        }
        public IActionResult Delete(int id) 
        { 
            var course = context.Courses.Find( id );
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.Department = new SelectList(
                context.Departments,
                "Id",
                "Name",
                course.DepartmentId
                );
            return View(course);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id) { 
            var course = context.Courses.Find(id);
            if (course == null) {
                return Json(new
                {
                    success = false,
                    message = "Course not found."

                });
            }
            context.Courses.Remove(course);
            context.SaveChanges();
            return Json (new
            {
                success = true,
                message = "Course deleted successfully."
            });
        }
         
    }
}
