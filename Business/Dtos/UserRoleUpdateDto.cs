
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class UserRoleUpdateDto
{
    [Required]
    public string RoleName { get; set; } = null!;
}
