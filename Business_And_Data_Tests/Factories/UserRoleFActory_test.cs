
using Business.Dtos;
using Business.Factories;
using Business.Models;
using Data.Entities;

namespace Business_And_Data_Tests.Factories;

public class UserRoleFActory_test
{
    [Fact]
    public void Create_ShouldReturnNewUserRoleCreateDto()
    {
        // Act
        var dto = UserRoleFactory.Create();

        // Assert
        Assert.NotNull(dto);
    }

    [Fact]
    public void CreateUserRoleEntity_WithValidDto_ShouldReturnEntity_WithCorrectValues()
    {
        // Arrange
        var createDto = new UserRoleCreateDto
        {
            RoleName = "Manager"
        };

        // Act
        var entity = UserRoleFactory.CreateUserRoleEntity(createDto);

        // Assert
        Assert.NotNull(entity);
        Assert.Equal(createDto.RoleName, entity.RoleName);
    }

    [Fact]
    public void CreateUserRoleEntity_NullDto_ShouldThrowArgumentNullException()
    {
        // Arrange
        UserRoleCreateDto createDto = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => UserRoleFactory.CreateUserRoleEntity(createDto));
    }

    [Fact]
    public void CreateUserRoleModel_WithValidEntity_ShouldReturnModel_WithCorrectValues()
    {
        // Arrange
        var entity = new UserRoleEntity
        {
            Id = 5,
            RoleName = "Administrator"
        };

        // Act
        var model = UserRoleFactory.Create(entity);

        // Assert
        Assert.NotNull(model);
        Assert.Equal(entity.Id, model.Id);
        Assert.Equal(entity.RoleName, model.RoleName);
    }

    [Fact]
    public void UpdateUserRoleEntity_WithValidData_ShouldUpdateEntity_WithCorrectValues()
    {
        // Arrange
        var entity = new UserRoleEntity
        {
            Id = 10,
            RoleName = "User"
        };

        var updateDto = new UserRoleUpdateDto
        {
            RoleName = "SuperUser"
        };

        // Act
        var updatedEntity = UserRoleFactory.UpdateUserRoleEntity(entity, updateDto);

        // Assert
        Assert.NotNull(updatedEntity);
        Assert.Equal(entity.Id, updatedEntity.Id);
        Assert.Equal(updateDto.RoleName, updatedEntity.RoleName);
    }
}
