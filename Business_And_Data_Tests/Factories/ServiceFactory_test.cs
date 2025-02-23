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
