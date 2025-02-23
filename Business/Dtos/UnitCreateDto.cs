using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class UnitCreateDto
{
    [Required]
    public string UnitName { get; set; } = null!;
}
