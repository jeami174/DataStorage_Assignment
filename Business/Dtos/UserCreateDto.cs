using System.ComponentModel.DataAnnotations;

namespace Business.Models;

public class UserCreateDto
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = null!;

    public int RoleId { get; set; }
}
