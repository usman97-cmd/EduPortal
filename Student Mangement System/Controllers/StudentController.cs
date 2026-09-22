using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Mangement_System.Data;
using Student_Mangement_System.Models;
using Student_Mangement_System.Services;
using Student_Mangement_System.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Student_Mangement_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentController(IStudentService studentService) : Controller
    {
        public IActionResult Index()
        {
            var student = studentService.Getall();
            return View(student);
        }
        
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(
                  studentService.GetDepartments(),
                  "Id",
                  "Name"
            );

            return View();

        }

        [HttpPost]
        public async Task <IActionResult> Create(StudentCreateViewModel vm)
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

            await studentService.Create(vm);

            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var student = studentService.Getbyid(id);

            if (student == null)
            {
                return NotFound();
            }

            ViewBag.Departments = new SelectList(
                studentService.GetDepartments(),
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
                studentService.Update(student);
                return RedirectToAction("Index");
            }
            ViewBag.Departments = new SelectList(
                studentService.GetDepartments(),
                "Id",
                "Name",
                student.DepartmentId
            );
            return View(student);
        }
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = studentService.Delete(id);

            return Json(new
            {
                success = result.Success,
                message = result.Message
            });
        }

    }
}
