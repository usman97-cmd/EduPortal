using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Mangement_System.Data;

namespace Student_Mangement_System.Controllers
{
    public class TeacherController (AppDbContext context): Controller
    {
        public IActionResult Index()
        {
            var Teacher = context.Teachers.
                          Include(d =>d.Department)
                          .ToList();
            return View(Teacher);
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
        public IActionResult Create(Teacher teacher) 
        {
            if (ModelState.IsValid)
            {
                context.Teachers.Add(teacher);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = new SelectList(
               context.Departments,
               "Id",
               "Name" 
           );
            return View(teacher);
        }
        public IActionResult Edit(int id)
        {
            var Teacher = context.Teachers.Find(id);
            if (Teacher == null)
            {
                return NotFound();
            }
            ViewBag.Departments = new SelectList
                (
                context.Departments,
                "Id",
                "Name",
                Teacher.DepartmentId
                );
            return View(Teacher);
        }
        [HttpPost]
        public IActionResult Edit(Teacher teacher) 
        {
            if (ModelState.IsValid) 
            {
                context.Teachers.Update(teacher);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Departments = new SelectList(
               context.Departments,
               "Id",
               "Name",
               teacher.DepartmentId
           );
            return View(teacher);
        }
        public IActionResult Delete(int id)
        {
            var teacher = context.Teachers
                          .Include(s => s.Department)
                          .FirstOrDefault(s => s.Id == id);
            if(teacher == null)
            {
                return NotFound();
            }
            return View (teacher);
        }
        
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var teacher = context.Teachers.Find(id);

            if (teacher == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Teacher not found."
                });
            }

            context.Teachers.Remove(teacher);
            context.SaveChanges();

            return Json(new
            {
                success = true,
                message = "Teacher deleted successfully."
            });
        }
    }
}
