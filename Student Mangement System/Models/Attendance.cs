using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Mangement_System.Models;

public class Attendance
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

    [DataType(DataType.Date)]
    public DateTime AttendanceDate { get; set; }

    public bool IsPresent { get; set; }
}