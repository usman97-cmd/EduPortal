using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Student_Mangement_System.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Teacher
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    [EmailAddress]
    public string Email { get; set; } = null!;

    [Phone]
    public string Phone { get; set; } = null!;
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }

    [ValidateNever]
    public Department Department { get; set; } = null!;
}