using Business.Factories;
using Data.Entities;

namespace Business_And_Data_Tests.Factories;

public class UnitFactory_test
{
    [Fact]
    public void CreateUnitModel_ShouldReturnModel_WithCorrectValues()
    {
        // Arrange
        var entity = new UnitEntity
        {
            Id = 1,
            UnitName = "Hours"
        };

        // Act
        var model = UnitFactory.CreateUnitModel(entity);

        // Assert
        Assert.NotNull(model);
        Assert.Equal(entity.Id, model.Id);
        Assert.Equal(entity.UnitName, model.UnitName);
    }

}
