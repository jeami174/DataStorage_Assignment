using System.Data;
using System.Net.NetworkInformation;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class UserRoleFactory
{
    public static UserRoleCreateDto Create()
    {
        return new UserRoleCreateDto();
    }

    public static UserRoleEntity CreateUserRoleEntity(UserRoleCreateDto dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        return new UserRoleEntity
        {
            RoleName = dto.RoleName
        };
    }

    public static UserRoleModel Create(UserRoleEntity entity)
    {
        var users = new List<UserModel>();

        foreach (var row in entity.Users)
        {
            users.Add(new UserModel()
            {
                Id = row.Id,
                FirstName = row.FirstName,
                LastName = row.LastName,
                Email = row.Email,
                RoleId = row.RoleId,
            });
        }


        return new UserRoleModel()
        {
            Id = entity.Id,
            RoleName = entity.RoleName,
            Users = users
        };
    }

    public static UserRoleEntity UpdateUserRoleEntity(UserRoleEntity entity, UserRoleUpdateDto dto)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        entity.RoleName = dto.RoleName;
        return entity;
    }
}
