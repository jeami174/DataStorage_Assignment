using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Data.Entities;

[Index(nameof(StatusTypeName), IsUnique = true)]
public class StatusTypeEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(50)")]
    public string StatusTypeName { get; set; } = null!;

    public ICollection<ProjectEntity> Projects { get; set; } = [];
}
