using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Student_Mangement_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Student_Mangement_System.Models;

public class Enrollment
{
    public int Id { get; set; }
    [ForeignKey("Student")]
    public int StudentId { get; set; }
    [ValidateNever]
    public Student Student { get; set; } = null!;
    [ForeignKey("Course")]
    public int CourseId { get; set; }
    [ValidateNever]
    public Course Course { get; set; } = null!;

    public DateTime EnrollmentDate { get; set; } = DateTime.Now;
}