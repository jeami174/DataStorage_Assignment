﻿namespace Business.Models;

public class UserRoleModel
{
    public int Id { get; set; }
    public string RoleName { get; set; } = null!;

    public ICollection<UserModel> Users { get; set; } = null!;
}
