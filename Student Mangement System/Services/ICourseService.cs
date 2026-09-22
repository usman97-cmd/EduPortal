using Student_Mangement_System.Models;

namespace Student_Mangement_System.Services
{
    public interface ICourseService
    {
        List<Course> GetCourses();
        void Create(Course course);

        void Update(Course course);
        bool Delete(int id);
        List<Department> GetDepartments();
        Course? Getbyid(int id);
    }

}
