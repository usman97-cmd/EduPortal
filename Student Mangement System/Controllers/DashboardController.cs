using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student_Mangement_System.Data;
using Student_Mangement_System.ViewModels;

namespace Student_Mangement_System.Controllers
{
    public class DashboardController(AppDbContext context) : Controller
    {
        public IActionResult Index()
        {
            var totalStudents = context.Students.Count();

            var totalCourses = context.Courses.Count();

            var totalTeachers = context.Teachers.Count();

            var totalDepartments = context.Departments.Count();

            var totalEnrollments = context.Enrollments.Count();

            var departmentReport = context.Students
               .Include(s => s.Department)
               .GroupBy(s => s.Department)
               .Select(group => new DepartmentStudentReportViewModel
               {
                   DepartmentName = group.Key.Name,
                   StudentCount = group.Count(),
                   StudentNames = group.Select(s => s.FirstName + " " + s.LastName).ToList()
               })
               .ToList();
            var courseReport = context.Enrollments
                 .Include(e => e.Course)
                 .GroupBy(e => e.Course)
                 .Select(group => new CourseViewModel
                 {
                     CourseName = group.Key.CourseName,
                     EnrollmentCount = group.Count()
                 })
                .ToList();

            var dashboard = new DashboardViewModel
            {
                TotalStudents = totalStudents,
                TotalCourses = totalCourses,
                TotalTeachers = totalTeachers,
                TotalDepartments = totalDepartments,
                TotalEnrollments = totalEnrollments,
                DepartmentReport = departmentReport,
                CourseReport = courseReport
            };
           
            return View(dashboard);
        }
    }
}
