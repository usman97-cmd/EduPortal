using System.ComponentModel.DataAnnotations;

public class Attendance
{
    public int Id { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    [DataType(DataType.Date)]
    public DateTime AttendanceDate { get; set; }

    public bool IsPresent { get; set; }
}