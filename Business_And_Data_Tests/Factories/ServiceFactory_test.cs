using Business.Dtos;
using Business.Factories;
using Data.Entities;

namespace Business.Tests.Factories;

public class ServiceFactory_Tests
{
    [Fact]
    public void Create_ShouldReturn_New_ServiceCreateDto()
    {
        // Act
        var dto = ServiceFactory.Create();

        // Assert
        Assert.NotNull(dto);
    }

    [Fact]
    public void CreateServiceEntity_ShouldMap_PropertiesCorrectly()
    {
        // Arrange
        var dto = new ServiceCreateDto
        {
            ServiceName = "Test Service",
            PricePerUnit = 99.99m,
            UnitId = 5
        };

        // Act
        var entity = ServiceFactory.CreateServiceEntity(dto);

        // Assert
        Assert.NotNull(entity);
        Assert.Equal(dto.ServiceName, entity.ServiceName);
        Assert.Equal(dto.PricePerUnit, entity.PricePerUnit);
        Assert.Equal(dto.UnitId, entity.UnitId);
    }

    [Fact]
    public void CreateServiceModel_ShouldMap_PropertiesAndProjectsCorrectly()
    {
        // Arrange
        var unit = new UnitEntity
        {
            Id = 1,
            UnitName = "Hour"
        };

        var project1 = new ProjectEntity
        {
            Title = "Project 1",
            Description = "Description 1",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(10),
            QuantityofServiceUnits = 2,
            TotalPrice = 200m,
            CustomerId = 1,
            StatusId = 1,
            UserId = 1,
            ServiceId = 1
        };
        var project2 = new ProjectEntity
        {
            Title = "Project 2",
            Description = "Description 2",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(20),
            QuantityofServiceUnits = 3,
            TotalPrice = 300m,
            CustomerId = 2,
            StatusId = 2,
            UserId = 2,
            ServiceId = 1
        };

        var entity = new ServiceEntity
        {
            Id = 1,
            ServiceName = "Test Service",
            PricePerUnit = 99.99m,
            UnitId = 1,
            Unit = unit,
            Projects = new List<ProjectEntity> { project1, project2 }
        };

        // Act
        var model = ServiceFactory.CreateServiceModel(entity);

        // Assert
        Assert.NotNull(model);
        Assert.Equal(entity.Id, model.Id);
        Assert.Equal(entity.ServiceName, model.ServiceName);
        Assert.Equal(entity.PricePerUnit, model.PricePerUnit);
        Assert.Equal(entity.UnitId, model.UnitId);
        Assert.NotNull(model.Unit);
        Assert.Equal(unit.Id, model.Unit.Id);
        Assert.Equal(unit.UnitName, model.Unit.UnitName);

        Assert.NotNull(model.Projects);
        Assert.Equal(entity.Projects.Count, model.Projects.Count);
        var projModel = model.Projects.First();
        Assert.Equal(project1.Title, projModel.Title);
        Assert.Equal(project1.Description, projModel.Description);
        Assert.Equal(project1.QuantityofServiceUnits, projModel.QuantityofServiceUnits);
        Assert.Equal(project1.TotalPrice, projModel.TotalPrice);
        Assert.Equal(project1.CustomerId, projModel.CustomerId);
        Assert.Equal(project1.StatusId, projModel.StatusId);
        Assert.Equal(project1.UserId, projModel.UserId);
        Assert.Equal(project1.ServiceId, projModel.ServiceId);
    }

    [Fact]
    public void UpdateServiceEntity_ShouldUpdate_PropertiesCorrectly()
    {
        // Arrange
        var entity = new ServiceEntity
        {
            ServiceName = "Old Service",
            PricePerUnit = 50m,
            UnitId = 1
        };

        var updateDto = new ServiceUpdateDto
        {
            ServiceName = "Updated Service",
            PricePerUnit = 75m,
        };

        // Act
        ServiceFactory.UpdateServiceEntity(entity, updateDto);

        // Assert
        Assert.Equal(updateDto.ServiceName, entity.ServiceName);
        Assert.Equal(updateDto.PricePerUnit, entity.PricePerUnit);
    }
}
