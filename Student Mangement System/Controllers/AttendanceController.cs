using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Mangement_System.Data;
using Student_Mangement_System.Migrations;
using Student_Mangement_System.Models;

namespace Student_Mangement_System.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AttendanceController(AppDbContext context) : Controller
    {
        public IActionResult Index()
        {
            var attendance = context.Attendances
                             .Include(s => s.Student)
                             .Include(c => c.Course)
                             .ToList();
            return View(attendance);
        }
        public IActionResult Create()
        {
            ViewBag.Students = new SelectList(
                context.Students,
                "Id",
                "FirstName"
            );
            ViewBag.Courses = new SelectList(
                context.Courses,
                "Id",
                "CourseName"
            );
            return View();
        }
        [HttpPost]
        public IActionResult Create(Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                context.Attendances.Add(attendance);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Students = new SelectList(
               context.Students,
               "Id",
               "FirstName"
           );
            ViewBag.Courses = new SelectList(
                context.Courses,
                "Id",
                "CourseName"
            );
            return View(attendance);

        }
        public IActionResult Edit(int id)
        {
            var attendance = context.Attendances.Find(id);
            if (attendance == null)
            {
                return NotFound();
            }
            ViewBag.Students = new SelectList(
             context.Students,
             "Id",
             "FirstName",
             attendance.StudentId
            );
            ViewBag.Courses = new SelectList(
                context.Courses,
                "Id",
                "CourseName",
                attendance.CourseId
            );
            return View(attendance);
        }
        [HttpPost]
        public IActionResult Edit(Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                context.Attendances.Update(attendance);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Students = new SelectList(
              context.Students,
              "Id",
              "FirstName",
              attendance.StudentId
          );
            ViewBag.Courses = new SelectList(
                context.Courses,
                "Id",
                "CourseName",
                attendance.CourseId
            );
            return View(attendance);
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var attendance = context.Attendances.Find(id);
            if (attendance == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Attendance not found."
                });
            }
            context.Attendances.Remove(attendance);
            context.SaveChanges();
            return Json(new 
            {
                success = true,
                message = "Result record removed successfully."
            });
        }
    }
}
