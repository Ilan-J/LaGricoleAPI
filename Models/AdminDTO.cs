using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaGricoleAPI.Models;

[Table("employee")]
public class AdminDTO
{
    [Key]
    [Column("pk_employee")]
    public int Pk_employee  { get; set; }
    [Required]
    [Column("email")]
    public string Email     { get; set; }
    [Required]
    [Column("password")]
    public string Password  { get; set; }
}