using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaGricoleAPI.Models;

[Table("department")]
public class DepartmentDTO
{
    [Key]
    [Column("pk_department")]
	public int Pk_department { get; set; }
    [Required]
    [Column("name")]
	public string Name { get; set; }
}
