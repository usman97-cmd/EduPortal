using Student_Mangement_System.Models;

namespace Student_Mangement_System.Services
{
    public interface ITeacherService
    {
        List<Teacher> Getall();
        Teacher? Getbyid(int id);
        List<Department> GetDepartments();
        void Create(Teacher teacher);
        void Update(Teacher teacher);
        bool Delete(int id);
    }
}
