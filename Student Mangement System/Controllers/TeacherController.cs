using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Mangement_System.Data;
using Student_Mangement_System.Services;

namespace Student_Mangement_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TeacherController (ITeacherService teacherService): Controller
    {
        public IActionResult Index()
        {
            var Teacher = teacherService.Getall();
            return View(Teacher);
        }
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(
                teacherService.GetDepartments(),
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
                teacherService.Create(teacher);
                return RedirectToAction("Index");
            }
            ViewBag.Departments = new SelectList(
              teacherService.GetDepartments(),
               "Id",
               "Name" 
           );
            return View(teacher);
        }
        public IActionResult Edit(int id)
        {
            var Teacher = teacherService.Getbyid(id);
            if (Teacher == null)
            {
                return NotFound();
            }
            ViewBag.Departments = new SelectList
                (
                teacherService.GetDepartments(),
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
                teacherService.Update(teacher);
                return RedirectToAction("Index");
            }
            ViewBag.Departments = new SelectList(
               teacherService.GetDepartments(),
               "Id",
               "Name",
               teacher.DepartmentId
           );
            return View(teacher);
        }
        public IActionResult Delete(int id)
        {
            var teacher = teacherService.Getbyid(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View (teacher);
        }
        
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var teacher = teacherService.Getbyid(id);

            if (teacher == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Teacher not found."
                });
            }

            teacherService.Delete(id);

            return Json(new
            {
                success = true,
                message = "Teacher deleted successfully."
            });
        }
    }
}
