using Microsoft.AspNetCore.Http;

namespace Student_Mangement_System.ViewModels
{
    public class StudentCreateViewModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string RegistrationNo { get; set; } = null!;
        public string CNIC { get; set; } = null!;

        public DateTime AdmissionDate { get; set; }
        public bool IsActive { get; set; }

        public string Phone { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; } = null!;
        public string Address { get; set; } = null!;

        public int DepartmentId { get; set; }

        public IFormFile? Photo { get; set; }
    }
}