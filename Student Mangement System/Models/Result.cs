using System.ComponentModel.DataAnnotations;

public class Result
{
    public int Id { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public decimal MidMarks { get; set; }

    public decimal FinalMarks { get; set; }

    public decimal TotalMarks { get; set; }

    [StringLength(2)]
    public string Grade { get; set; } = null!;
}