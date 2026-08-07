using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Student_Mangement_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Student
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    public string RegistrationNo { get; set; } = null!;
    public string CNIC { get; set; } = null!;
    public string Photo { get; set; } = null!;
    public DateTime AdmissionDate { get; set; }
    public bool IsActive { get; set; }
    [Phone]
    public string Phone { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = null!;
    public string Address { get; set; } = null!;
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }

    [ValidateNever]
    public Department Department { get; set; } = null!;

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public ICollection<Result> Results { get; set; } = new List<Result>();

    public ICollection<Fee> Fees { get; set; } = new List<Fee>();
}