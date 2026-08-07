using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Mangement_System.Data;
using Student_Mangement_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Mangement_System.Controllers
{
    public class StudentController(AppDbContext context) : Controller
    {
        public IActionResult Index()
        {
            var student = context.Students
                         .Include(s => s.Department)
                         .ToList();
            return View(student);
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
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new
                    {
                        Field = x.Key,
                        Errors = x.Value.Errors.Select(e => e.ErrorMessage)
                    });

                return Json(errors);
            }

            context.Students.Add(student);
            context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var student = context.Students.Find(id);

            if (student == null)
            {
                return NotFound();
            }

            ViewBag.Departments = new SelectList(
                context.Departments,
                "Id",
                "Name",
                student.DepartmentId
            );

            return View(student);
        }
        [HttpPost]
        public IActionResult Edit(Student student)
        {
            if (ModelState.IsValid) 
            {
                context.Students.Update(student);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = new SelectList(
                context.Departments,
                "Id",
                "Name",
                student.DepartmentId
            );
            return View(student);
        }
        public IActionResult Delete(int id)
        {
            var student = context.Students
              .Include(s => s.Department)
              .FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = context.Students.Find(id);

            if (student == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Student not found."
                });
            }

            context.Students.Remove(student);
            context.SaveChanges();

            return Json(new
            {
                success = true,
                message = "Student deleted successfully."
            });
        }

    }
}
