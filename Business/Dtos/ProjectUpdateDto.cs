using System.ComponentModel.DataAnnotations;
namespace Business.Dtos;

public class ProjectUpdateDto
{
    [Required]
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public int Status { get; set; }
}
