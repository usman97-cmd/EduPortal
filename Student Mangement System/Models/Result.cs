using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Student_Mangement_System.Models;

public class Result
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

    public decimal MidMarks { get; set; }

    public decimal FinalMarks { get; set; }

    public decimal TotalMarks { get; set; }

    [StringLength(2)]
    public string Grade { get; set; } = null!;
}