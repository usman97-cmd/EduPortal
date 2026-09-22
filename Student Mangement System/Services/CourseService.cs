using Microsoft.EntityFrameworkCore;
using Student_Mangement_System.Data;
using Student_Mangement_System.Models;

namespace Student_Mangement_System.Services
{
    public class CourseService(AppDbContext context) : ICourseService
    {
        public List<Course> GetCourses()
        {
            return context.Courses
                          .Include(s => s.Department)
                          .ToList();
        }
        public void Create(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();
        }
        public void Update(Course course)
        {
            context.Courses.Update(course);
            context.SaveChanges();
        }
        public Course? Getbyid(int id)
        {
            return context.Courses.Find(id);

        }
        public bool Delete(int id)
        {
            var course = context.Courses.Find(id);
            if (course == null)
            {
                return false;
            }
            context.Courses.Remove(course);
            context.SaveChanges();
            return true;

        }
        public List<Department> GetDepartments()
        {
            return context.Departments.ToList();
        }
    }
}
