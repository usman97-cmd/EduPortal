using Student_Mangement_System.Models;

namespace Student_Mangement_System.Services
{
    public interface IEnrollmentService
    {
        List<Enrollment> GetAll();
        void Create(Enrollment enrollment);
        void Update(Enrollment enrollment);
        bool Delete(int id);
        List<Student> GetAllStudents();
        List<Course> GetAllCourses();
        Enrollment? Getbyid(int id);

    }
}
