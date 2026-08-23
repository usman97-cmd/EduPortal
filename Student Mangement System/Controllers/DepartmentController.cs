using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student_Mangement_System.Data;
using Student_Mangement_System.Models;
using Student_Mangement_System.Services;

namespace Student_Mangement_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DepartmentController(IDepartmentService departmentService) : Controller
    {
        public IActionResult Index()
        {
            var departments = departmentService.GetAll();
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
                departmentService.Create(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }
        public IActionResult Edit(int id)
        {
            var department = departmentService.Getbyid(id);
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
                departmentService.Update(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }
        public IActionResult Delete(int id)
        {
            var department = departmentService.Getbyid(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
                var department = departmentService.Getbyid(id);
            if (department == null)
                {
                return Json(new
                {
                    success = false,
                    message = "Department Not Found."

                });
                }
            departmentService.Delete(id);
            return Json(new { 
                success = true,
                message ="Department deleted successfully."
            });

        }
    }
}
