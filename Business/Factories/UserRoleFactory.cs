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

    public static UserRoleModel CreateUserRoleModel(UserRoleEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        return new UserRoleModel
        {
            Id = entity.Id,
            RoleName = entity.RoleName
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
