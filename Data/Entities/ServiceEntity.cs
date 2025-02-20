using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(ServiceName), IsUnique = true)]
public class ServiceEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string ServiceName { get; set; } = null!;

    [Required]
    public decimal PricePerUnit{ get; set; }


    [ForeignKey("Unit")]
    [Required]
    public int UnitId { get; set; }
    public UnitEntity Unit { get; set; } = null!;

    public ICollection<ProjectEntity> Projects { get; set; } = [];

}
