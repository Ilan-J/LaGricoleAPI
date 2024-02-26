using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LaGricoleAPI.Models;

[Table("location")]
public class LocationDTO
{
    [Key]
    [Column("pk_location")]
    public int Pk_location { get; set; }
    [Required]
    [Column("city")]
    public string City { get; set; }
}