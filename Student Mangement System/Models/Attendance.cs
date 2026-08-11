using System.ComponentModel.DataAnnotations;

namespace Student_Mangement_System.Models;

public class Attendance
{
    public int Id { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

    [DataType(DataType.Date)]
    public DateTime AttendanceDate { get; set; }

    public bool IsPresent { get; set; }
}