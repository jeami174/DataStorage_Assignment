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

    public static UserModel Create(UserEntity entity)
    {
        var projects = new List<ProjectModel>();

        foreach (var row in entity.Projects)
        {
            projects.Add(new ProjectModel()
            {
                Title = row.Title,
                Description = row.Description,
                StartDate = row.StartDate,
                EndDate = row.EndDate,
                QuantityofServiceUnits = row.QuantityofServiceUnits,
                TotalPrice = row.TotalPrice,
                CustomerId = row.CustomerId,
                StatusId = row.StatusId,
                UserId = row.UserId,
                ServiceId = row.ServiceId
            });
        }

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
