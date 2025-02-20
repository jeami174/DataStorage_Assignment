using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class UserRoleCreateDto
{
    [Required]
    public string RoleName { get; set; } = null!;
}
