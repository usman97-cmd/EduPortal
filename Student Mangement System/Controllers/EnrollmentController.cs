using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Mangement_System.Data;
using Student_Mangement_System.Models;

namespace Student_Mangement_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EnrollmentController(AppDbContext context) : Controller
    {
        public IActionResult Index()
        {
            var enrollment = context.Enrollments
                          .Include(s => s.Student)
                          .Include(c => c.Course)
                          .ToList();
            return View(enrollment);
        }
        public IActionResult Create()
        {
            ViewBag.Students = new SelectList(
                context.Students,
                "Id",
                "FirstName"
            );
            ViewBag.Courses = new SelectList
                (
                   context.Courses,
                   "Id",
                   "CourseName"
                );

            return View();
        }
        [HttpPost]
        public IActionResult Create (Enrollment enrollment) 
        {
            if (ModelState.IsValid)
            {
                context.Enrollments.Add(enrollment);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Students = new SelectList(
               context.Students,
               "Id",
               "FirstName"
           );
            ViewBag.Courses = new SelectList
                (
                   context.Courses,
                   "Id",
                   "CourseName"
                );
            return View (enrollment);
        }
        public IActionResult Edit(int id)
        {
            var enrollment = context.Enrollments.Find(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            ViewBag.Students = new SelectList(
                context.Students,
                "Id",
                "FirstName",
                enrollment.StudentId
            );

            ViewBag.Courses = new SelectList(
                context.Courses,
                "Id",
                "CourseName",
                enrollment.CourseId
            );

            return View(enrollment);
        }
        [HttpPost]
        public IActionResult Edit(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                context.Enrollments.Update(enrollment);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Students = new SelectList(
                context.Students,
                "Id",
                "FirstName",
                enrollment.StudentId
            );

            ViewBag.Courses = new SelectList(
                context.Courses,
                "Id",
                "CourseName",
                enrollment.CourseId
            );

            return View(enrollment);
        }
        [HttpDelete]
        public IActionResult DeleteConfirmed(int id)
        {
            var enrollment = context.Enrollments.Find(id);

            if (enrollment == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Enrollment not found."
                });
            }

            context.Enrollments.Remove(enrollment);
            context.SaveChanges();

            return Json(new
            {
                success = true,
                message = "Enrollment deleted successfully."
            });
        }
    }
}
