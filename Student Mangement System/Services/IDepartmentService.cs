using Student_Mangement_System.Models;

namespace Student_Mangement_System.Services
{
    public interface IDepartmentService
    {
        List<Department> GetAll();
        Department? Getbyid(int id);
        void Create(Department department);
        void Update(Department department);
        bool Delete(int id);
    }
}
