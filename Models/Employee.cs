using System.ComponentModel.DataAnnotations;

namespace HRMWeb.Models;

public class Employee
{
    public int EmployeeId { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Division { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Building { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Room { get; set; } = string.Empty;
}