using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Mangement_System.Data;
using Student_Mangement_System.ViewModels;

namespace Student_Mangement_System.Controllers
{
    public class ReportController(AppDbContext context) : Controller
    {
        public IActionResult Index()
        {
            var Departmentreport = context.Students
                .Include(s => s.Department)
                .GroupBy(s => s.Department)
                .Select(group => new DepartmentStudentReportViewModel {
                    DepartmentName = group.Key.Name,
                    StudentCount = group.Count(),
                    StudentNames = group
                   .Select(s => s.FirstName)
                   .ToList()
                })
            .ToList();
            var Coursereport = context.Enrollments
                .Include(s => s.Course)
                .GroupBy(s => s.Course)
                .Select(group => new CourseViewModel
                {
                    CourseName = group.Key.CourseName,
                    EnrollmentCount = group.Count(),
                })
                .ToList();
            var report = new ReportViewModel
            { 
                DepartmentReport = Departmentreport,
                CourseReport = Coursereport
            };
            return View(report);
        }
    }
}
