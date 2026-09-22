using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Mangement_System.Data;
using Student_Mangement_System.Models;
using Student_Mangement_System.Services;

namespace Student_Mangement_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EnrollmentController(IEnrollmentService enrollmentService) : Controller
    {
        public IActionResult Index()
        {
            var enrollment = enrollmentService.GetAll();
            return View(enrollment);
        }
        public IActionResult Create()
        {
            ViewBag.Students = new SelectList(
                enrollmentService.GetAllStudents(),
                "Id",
                "FirstName"
            );
            ViewBag.Courses = new SelectList
                (
                   enrollmentService.GetAllCourses(),
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
               enrollmentService.Create(enrollment);
                return RedirectToAction("Index");
            }
            ViewBag.Students = new SelectList(
               enrollmentService.GetAllStudents(),
               "Id",
               "FirstName"
           );
            ViewBag.Courses = new SelectList
                (
                   enrollmentService.GetAllCourses(),
                   "Id",
                   "CourseName"
                );
            return View (enrollment);
        }
        public IActionResult Edit(int id)
        {
            var enrollment = enrollmentService.Getbyid(id);

            if (enrollment == null)
            {
                return NotFound();
            }

            ViewBag.Students = new SelectList(
                enrollmentService.GetAllStudents(),
                "Id",
                "FirstName",
                enrollment.StudentId
            );

            ViewBag.Courses = new SelectList(
                enrollmentService.GetAllCourses(),
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
                enrollmentService.Update(enrollment);

                return RedirectToAction("Index");
            }

            ViewBag.Students = new SelectList(
               enrollmentService.GetAllStudents(),
                "Id",
                "FirstName",
                enrollment.StudentId
            );

            ViewBag.Courses = new SelectList(
                enrollmentService.GetAllCourses(),
                "Id",
                "CourseName",
                enrollment.CourseId
            );

            return View(enrollment);
        }
        [HttpDelete]
        public IActionResult DeleteConfirmed(int id)
        {
            var enrollment = enrollmentService.Getbyid(id);
            if (enrollment == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Enrollment not found."
                });
            }

            enrollmentService.Delete(id);

            return Json(new
            {
                success = true,
                message = "Enrollment deleted successfully."
            });
        }
    }
}
