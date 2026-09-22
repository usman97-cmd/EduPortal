using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Mangement_System.Data;
using Student_Mangement_System.Models;
using Student_Mangement_System.Services;

namespace Student_Mangement_System.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CourseController(ICourseService courseService): Controller
    {
        public IActionResult Index()
        {
            var courses = courseService.GetCourses();
            return View(courses);
        }
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(
               courseService.GetDepartments(),
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
                courseService.Create(course);
                return RedirectToAction("Index");
            }
            ViewBag.Departments = new SelectList(
             courseService.GetDepartments(),
              "Id",
              "Name"
              );
            return View(course);

        }
        public IActionResult Edit(int id)
        {
            var courses = courseService.Getbyid(id);
            if( courses == null)
            {
                return NotFound();
            }
            ViewBag.Department = new SelectList(
                courseService.GetDepartments(),
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
                courseService.Update(course);
                return RedirectToAction("Index");
            }
            ViewBag.Department = new SelectList(
               courseService.GetDepartments(),
                "Id",
                "Name",
                course.DepartmentId
                );
            return View(course);

        }
        public IActionResult Delete(int id) 
        {
            var course = courseService.Getbyid(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.Department = new SelectList(
              courseService.GetDepartments(),
                "Id",
                "Name",
                course.DepartmentId
                );
            return View(course);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id) {
            var course = courseService.Getbyid(id);
            if (course == null) {
                return Json(new
                {
                    success = false,
                    message = "Course not found."

                });
            }
            courseService.Delete(id);
            return Json (new
            {
                success = true,
                message = "Course deleted successfully."
            });
        }
         
    }
}
