using Student_Mangement_System.Models;
using Student_Mangement_System.ViewModels;

namespace Student_Mangement_System.Services
{
    public interface IStudentService
    {
        List<Student> Getall();
        Student? Getbyid(int id);
        List<Department> GetDepartments();
        Task Create(StudentCreateViewModel vm);
        void Update(Student student);
        (bool Success, string Message) Delete(int id);

    }
}
