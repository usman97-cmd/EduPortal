using Microsoft.AspNetCore.Mvc;
using Student_Mangement_System.Data;
using Student_Mangement_System.Models;

namespace Student_Mangement_System.Controllers
{
    public class DepartmentController(AppDbContext context) : Controller
    {
        public IActionResult Index()
        {
            var departments = context.Departments.ToList();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                context.Departments.Add(department);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }
        public IActionResult Edit(int id)
        {
            var department = context.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpPost]
        public IActionResult Edit(Department department)
        {
            if (ModelState.IsValid)
            {
                context.Departments.Update(department);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }
        public IActionResult Delete(int id)
        {
            var department = context.Departments.Find(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
                var department = context.Departments.Find(id);
                if(department == null)
                {
                return Json(new
                {
                    success = false,
                    message = "Department Not Found."

                });
                }
             context.Departments.Remove(department);
             context.SaveChanges();
            return Json(new { 
                success = true,
                message ="Department deleted successfully."
            });

        }
    }
}
