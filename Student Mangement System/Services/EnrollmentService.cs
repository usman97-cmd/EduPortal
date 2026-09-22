using Microsoft.EntityFrameworkCore;
using Student_Mangement_System.Data;
using Student_Mangement_System.Models;

namespace Student_Mangement_System.Services
{
    public class EnrollmentService(AppDbContext context): IEnrollmentService
    {
        public List<Enrollment> GetAll()
        {
            return context.Enrollments
                          .Include(s => s.Student)
                          .Include(c => c.Course)
                          .ToList();
        }
        public List<Student> GetAllStudents()
        {
            return context.Students.ToList();
        }
        public List<Course> GetAllCourses()
        {
            return context.Courses.ToList();
        }
        public Enrollment? Getbyid (int id)
        {
            return context.Enrollments.Find(id);
        }
        public bool Delete(int id)
        {
            var enrollment = context.Enrollments.Find(id);
            if (enrollment == null)
            {
                return false;
            }
            context.Enrollments.Remove(enrollment);
            context.SaveChanges();
            return true;
        }
        public void Create(Enrollment enrollment)
        {
            context.Enrollments.Add(enrollment);
            context.SaveChanges();

        }
        public void Update(Enrollment enrollment)
        {
            context.Enrollments.Update(enrollment);
            context.SaveChanges ();
        }

    }
}
