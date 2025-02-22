using System.ComponentModel.DataAnnotations;
namespace Business.Dtos;

public class ProjectCreateDto
{
    [Required(ErrorMessage = "Please enter a titel")]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "The quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int QuantityofServiceUnits { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int ServiceId { get; set; }

    [Required]
    public int StatusId { get; set; }

    [Required]
    public int UserId { get; set; }
}
