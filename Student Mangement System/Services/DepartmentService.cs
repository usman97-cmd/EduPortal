using Student_Mangement_System.Data;
using Student_Mangement_System.Models;

namespace Student_Mangement_System.Services
{
    public class DepartmentService(AppDbContext context):IDepartmentService
    {
        public List<Department> GetAll()
        {
            return context.Departments.ToList();
        }
        public Department? Getbyid(int id)
        {
            return context.Departments.Find(id);

        }
        public void Create(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();
        }
        public void Update(Department department) 
        { 
            context.Departments.Update(department);
            context.SaveChanges();
        }
        public bool Delete(int id)
        {
            var department = context.Departments.Find(id);
            if (department == null)
            {
                return false;
            }
            context.Departments.Remove(department);
            context.SaveChanges();
            return true;
        }

    }
}
