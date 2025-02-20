using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class UserFactory
{
    public static UserCreateDto Create()
    {
        return new UserCreateDto();
    }

    public static UserEntity CreateUserEntity(UserCreateDto userCreateDto)
    {
        return new UserEntity
        {
            FirstName = userCreateDto.FirstName,
            LastName = userCreateDto.LastName,
            Email = userCreateDto.Email,
            RoleId = userCreateDto.RoleId
        };
    }

    public static UserModel CreateUserModel(UserEntity userEntity)
    {
        return new UserModel
        {
            Id = userEntity.Id,
            FirstName = userEntity.FirstName,
            LastName = userEntity.LastName,
            Email = userEntity.Email,
            Role = UserRoleFactory.CreateUserRoleModel(userEntity.Role)
        };
    }

    public static UserEntity UpdateUserEntity(UserEntity entity, UserUpdateDto dto)
    {
        entity.FirstName = dto.FirstName;
        entity.LastName = dto.LastName;
        entity.Email = dto.Email;
        entity.RoleId = dto.RoleId;
        return entity;
    }
}
