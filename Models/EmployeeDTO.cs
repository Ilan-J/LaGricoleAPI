using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaGricoleAPI.Models;

[Table("employee")]
public class EmployeeDTO
{
    [Key]
    [Column("pk_employee")]
    public int Pk_employee      { get; set; }
    [Required]
    [Column("lastname")]
    public string Lastname      { get; set; }
    [Required]
    [Column("firstname")]
    public string Firstname     { get; set; }
    [Required]
    [Column("phone")]
    public string Phone         { get; set; }
    [Column("cellphone")]
    public string Cellphone     { get; set; }
    [Required]
    [Column("email")]
    public string Email         { get; set; }
    [Required]
    [Column("fk_department")]
    public int Fk_department    { get; set; }
    [Required]
    [Column("fk_location")]
    public int Fk_location      { get; set; }
}
