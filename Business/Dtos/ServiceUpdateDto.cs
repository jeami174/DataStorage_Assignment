
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ServiceUpdateDto
{
    public string ServiceName { get; set; } = null!;

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price per unit must be greater than zero")]
    public decimal PricePerUnit { get; set; }
}
