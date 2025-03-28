﻿using Business.Dtos;
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

    public static UserModel Create(UserEntity entity)
    {
        var projects = new List<ProjectModel>();

        return new UserModel()
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Email = entity.Email,
            RoleId = entity.RoleId,
            Projects = projects,
            Role = UserRoleFactory.Create(entity.Role),
        };
    }

    public static UserEntity UpdateUserEntity(UserEntity entity, UserUpdateDto dto)
    {
        entity.FirstName = dto.FirstName;
        entity.LastName = dto.LastName;
        entity.Email = dto.Email;
        return entity;
    }
}
