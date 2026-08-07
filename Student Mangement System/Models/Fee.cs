using System.ComponentModel.DataAnnotations;

public class Fee
{
    public int Id { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public decimal Amount { get; set; }

    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? PaidDate { get; set; }

    public bool IsPaid { get; set; }
}