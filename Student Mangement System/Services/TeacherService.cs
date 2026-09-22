using Microsoft.EntityFrameworkCore;
using Student_Mangement_System.Data;
using Student_Mangement_System.Models;

namespace Student_Mangement_System.Services
{
    public class TeacherService(AppDbContext context):ITeacherService
    {
        public List<Teacher> Getall()
        {
            return context.Teachers.
                          Include(d => d.Department)
                          .ToList();
        }
        public Teacher? Getbyid(int id)
        {
            return context.Teachers
                          .Include(s => s.Department)
                          .FirstOrDefault(s => s.Id == id);
        }
        public List<Department> GetDepartments()
        {
            return context.Departments.ToList();
        }
        public void Create(Teacher teacher)
        {
            context.Teachers.Add(teacher);
            context.SaveChanges();
        }
        public void  Update(Teacher teacher)
        {
            context.Teachers.Update(teacher);
            context.SaveChanges();
        }
        public bool Delete(int id) 
        {
            var teacher = context.Teachers.Find(id);
            if (teacher == null)
            {
                return false;
            }
            context.Teachers.Remove(teacher);
            context.SaveChanges();
            return true;
        }

    }
}
