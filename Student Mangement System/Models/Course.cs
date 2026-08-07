using Student_Mangement_System.Models;
using System.ComponentModel.DataAnnotations;

public class Course
{
    public int Id { get; set; }

    [Required]
    public string CourseName { get; set; } = null!;

    public int CreditHours { get; set; }

    [DataType(DataType.Currency)]
    public decimal Fee { get; set; }

    public int DepartmentId { get; set; }

    public Department Department { get; set; } = null!;

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public ICollection<Result> Results { get; set; } = new List<Result>();
}