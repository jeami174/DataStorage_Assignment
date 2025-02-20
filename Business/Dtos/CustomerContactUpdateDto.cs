﻿using System.ComponentModel.DataAnnotations;
namespace Business.Dtos;

public class CustomerContactUpdateDto
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

}
