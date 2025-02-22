using System.ComponentModel.DataAnnotations;
namespace Business.Dtos;

public class CustomerUpdateDto
{
    [Required]
    public string CustomerName { get; set; } = null!;
}
