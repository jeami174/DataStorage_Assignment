using Business.Dtos;
using Business.Factories;
using Business.Models;
using Data.Entities;

namespace Business_And_Data_Tests.Factories;

public class UserFactory_test
{
    [Fact]
    public void Create_ShouldReturnNewUserCreateDto()
    {
        // Act
        var dto = UserFactory.Create();

        // Assert
        Assert.NotNull(dto);
    }

    [Fact]
    public void CreateUserEntity_ShouldReturnEntity_WithCorrectValues()
    {
        // Arrange
        var dto = new UserCreateDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            RoleId = 1
        };

        // Act
        var entity = UserFactory.CreateUserEntity(dto);

        // Assert
        Assert.NotNull(entity);
        Assert.Equal(dto.FirstName, entity.FirstName);
        Assert.Equal(dto.LastName, entity.LastName);
        Assert.Equal(dto.Email, entity.Email);
        Assert.Equal(dto.RoleId, entity.RoleId);
    }

    [Fact]
    public void CreateUserModel_ShouldReturnModel_WithCorrectValues()
    {
        // Arrange
        var roleEntity = new UserRoleEntity
        {
            Id = 1,
            RoleName = "Admin"
        };

        var userEntity = new UserEntity
        {
            Id = 10,
            FirstName = "Lennart",
            LastName = "Olsson",
            Email = "Lennart.Olsson@example.com",
            RoleId = 1,
            Role = roleEntity
        };

        // Act
        var model = UserFactory.Create(userEntity);

        // Assert
        Assert.NotNull(model);
        Assert.Equal(userEntity.Id, model.Id);
        Assert.Equal(userEntity.FirstName, model.FirstName);
        Assert.Equal(userEntity.LastName, model.LastName);
        Assert.Equal(userEntity.Email, model.Email);
        Assert.NotNull(model.Role);
        Assert.Equal(roleEntity.Id, model.Role.Id);
        Assert.Equal(roleEntity.RoleName, model.Role.RoleName);
    }

    [Fact]
    public void UpdateUserEntity_ShouldUpdateEntity_WithCorrectValues()
    {
        // Arrange
        var userEntity = new UserEntity
        {
            Id = 10,
            FirstName = "Micke",
            LastName = "Nyqvist",
            Email = "skadis@example.com",
            RoleId = 1
        };

        var updateDto = new UserUpdateDto
        {
            FirstName = "Marie",
            LastName = "Brunnhylt",
            Email = "lillan@example.com",
        };

        // Act
        var updatedEntity = UserFactory.UpdateUserEntity(userEntity, updateDto);

        // Assert
        Assert.Equal(userEntity.Id, updatedEntity.Id);
        Assert.Equal(updateDto.FirstName, updatedEntity.FirstName);
        Assert.Equal(updateDto.LastName, updatedEntity.LastName);
        Assert.Equal(updateDto.Email, updatedEntity.Email);
    }
}
