using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Student_Mangement_System.Models;

public class Fee
{
    public int Id { get; set; }
    [ForeignKey("Student")]
    public int StudentId { get; set; }
    [ValidateNever]
    public Student Student { get; set; } = null!;

    public decimal Amount { get; set; }

    [DataType(DataType.Date)]
    public DateTime DueDate { get; set; }

    [DataType(DataType.Date)]
    public DateTime? PaidDate { get; set; }

    public bool IsPaid { get; set; }
}