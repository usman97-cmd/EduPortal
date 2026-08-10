using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Student_Mangement_System.Models;

public class Course
{
    public int Id { get; set; }

    [Required]
    public string CourseName { get; set; } = null!;

    public int CreditHours { get; set; }

    [DataType(DataType.Currency)]
    public decimal Fee { get; set; }
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }
    [ValidateNever]
    public Department Department { get; set; } = null!;

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public ICollection<Result> Results { get; set; } = new List<Result>();
}