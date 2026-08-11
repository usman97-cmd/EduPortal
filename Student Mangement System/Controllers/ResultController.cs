using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Student_Mangement_System.Data;
using Student_Mangement_System.Models;

namespace Student_Mangement_System.Controllers
{
    public class ResultController(AppDbContext context) : Controller
    {
        public IActionResult Index()
        {
            var result = context.Results
                         .Include(x => x.Student)
                         .Include(x => x.Course)
                         .ToList();
            return View(result);
        }
        public IActionResult Create ()
        {
            ViewBag.Student = new SelectList(
                 context.Students,
                 "Id",
                 "FirstName"
                 
            );
            ViewBag.Course = new SelectList(
                context.Courses,
                "Id",
               "CourseName"
           );
            return View();

        }
        [HttpPost]
        public IActionResult Create (Result result)
        {
            result.TotalMarks = result.MidMarks + result.FinalMarks;
            if (result.TotalMarks >= 90)
            {
                result.Grade = "A+";
            }
            else if (result.TotalMarks >= 80)
            {
                result.Grade = "A";
            }
            else if (result.TotalMarks >= 70)
            {
                result.Grade = "B";
            }
            else if (result.TotalMarks >= 60)
            {
                result.Grade = "C";
            }
            else if (result.TotalMarks >= 50)
            {
                result.Grade = "D";
            }
            else
            {
                result.Grade = "F";
            }
            if (ModelState.IsValid)
            {
                context.Results.Add(result);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Student = new SelectList(
                context.Students,
                "Id",
                "FirstName",
                 result.StudentId

           );
            ViewBag.Course = new SelectList(
                context.Courses,
                "Id",
               "CourseName",
                result.CourseId
           );
            return View (result);
        }
        public IActionResult Edit(int id)
        {
            var result = context.Results.Find(id);
            if (result == null)
            {
                return NotFound();
            }
            ViewBag.Students = new SelectList(
               context.Students,
               "Id",
               "FirstName",
               result.StudentId
          );
            ViewBag.Courses = new SelectList(
                context.Courses,
                "Id",
               "CourseName",
               result.CourseId
           );
            return View(result);
        }
        [HttpPost]
        public IActionResult Edit(Result result)
        {
            result.TotalMarks = result.MidMarks + result.FinalMarks;
            if (result.TotalMarks >= 90)
            {
                result.Grade = "A+";
            }
            else if (result.TotalMarks >= 80)
            {
                result.Grade = "A";
            }
            else if (result.TotalMarks >= 70)
            {
                result.Grade = "B";
            }
            else if (result.TotalMarks >= 60)
            {
                result.Grade = "C";
            }
            else if (result.TotalMarks >= 50)
            {
                result.Grade = "D";
            }
            else
            {
                result.Grade = "F";
            }
            if (ModelState.IsValid)
            {
                context.Results.Update(result);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Student = new SelectList(
                context.Students,
                "Id",
                "FirstName",
                result.StudentId
            );

            ViewBag.Course = new SelectList(
                context.Courses,
                "Id",
                "CourseName",
                result.CourseId
            );

            return View(result);
        }
        [HttpDelete]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = context.Results.Find( id);
            if (result == null) {
                return Json(new
                {
                    success = false,
                    message = " Result not found."

                });
            }
            context.Results.Remove(result);
            context.SaveChanges();
            return Json(new { 
                success = true,
                message = "Result removed successfully."
            });



        }
    }
}
