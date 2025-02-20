using System.ComponentModel.DataAnnotations;
namespace Business.Dtos;

public class ProjectCreateDto
{
    [Required]
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    [Required]
    public int CustomerId { get; set; }
    [Required]
    public int ServiceId { get; set; }
    [Required]
    public int StatusId { get; set; }
    [Required]
    public int UserId { get; set; }
}
