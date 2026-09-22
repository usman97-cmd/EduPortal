using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Mangement_System.Data;
using Student_Mangement_System.Models;
using Student_Mangement_System.ViewModels;

namespace Student_Mangement_System.Services
{
    public class StudentService(AppDbContext context,IWebHostEnvironment environment):IStudentService
    {
        public List<Student> Getall()
        {
            return context.Students
                         .Include(s => s.Department)
                         .ToList();
        }
        public Student? Getbyid(int id)
        {
            return context.Students
              .Include(s => s.Department)
              .FirstOrDefault(s => s.Id == id);
        }
        public List<Department> GetDepartments()
        {
            return context.Departments.ToList();
        }
        public async Task Create(StudentCreateViewModel vm)
        {
            var uploadPath = Path.Combine(
                environment.WebRootPath,
                "uploads"
            );

            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            string? photoPath = null;

            if (vm.Photo != null)
            {
                var fileName = Guid.NewGuid().ToString()
                               + Path.GetExtension(vm.Photo.FileName);

                var filePath = Path.Combine(
                    uploadPath,
                    fileName
                );

                using var stream = new FileStream(
                    filePath,
                    FileMode.Create
                );

                await vm.Photo.CopyToAsync(stream);

                photoPath = "/uploads/" + fileName;
            }

            var student = new Student
            {
                FirstName = vm.FirstName,
                LastName = vm.LastName,
                Email = vm.Email,
                RegistrationNo = vm.RegistrationNo,
                CNIC = vm.CNIC,
                AdmissionDate = vm.AdmissionDate,
                IsActive = vm.IsActive,
                Phone = vm.Phone,
                DateOfBirth = vm.DateOfBirth,
                Gender = vm.Gender,
                Address = vm.Address,
                DepartmentId = vm.DepartmentId,
                Photo = photoPath
            };

            context.Students.Add(student);
            await context.SaveChangesAsync();
        }
        public void Update(Student student)
        {
            context.Students.Update(student);
            context.SaveChanges();
        }
        public (bool Success, string Message) Delete(int id)
        {
            var student = context.Students.Find(id);

            if (student == null)
            {
                return (false, "Student not found.");
            }

            if (context.Enrollments.Any(e => e.StudentId == id))
            {
                return (false, "Student cannot be deleted because enrollment record exists.");
            }

            if (context.Attendances.Any(a => a.StudentId == id))
            {
                return (false, "Student cannot be deleted because attendance record exists.");
            }

            if (context.Results.Any(r => r.StudentId == id))
            {
                return (false, "Student cannot be deleted because result record exists.");
            }

            if (context.Fees.Any(f => f.StudentId == id))
            {
                return (false, "Student cannot be deleted because fee record exists.");
            }

            context.Students.Remove(student);
            context.SaveChanges();

            return (true, "Student deleted successfully.");
        }
    }
}
