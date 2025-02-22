using Business.Factories;
using Data.Entities;

namespace Business_And_Data_Tests.Factories;

public class StatusTypeFactory_test
{
    [Fact]
    public void CreateStatusTypeModel_ShouldReturnModel_WithCorrectValues()
    {
        // Arrange
        var entity = new StatusTypeEntity
        {
            Id = 1,
            StatusTypeName = "Active"
        };

        // Act
        var model = StatusTypeFactory.CreateStatusTypeModel(entity);

        // Assert
        Assert.NotNull(model);
        Assert.Equal(entity.Id, model.Id);
        Assert.Equal(entity.StatusTypeName, model.StatusTypeName);
    }
}
