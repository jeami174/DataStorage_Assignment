using Business.Dtos;
using Business.Factories;
using Data.Entities;

namespace Business_And_Data_Tests.Factories;

public class CustomerContactFactory_test
{
    [Fact]
    public void CreateCustomerContactEntity_ShouldReturnEntity_WithCorrectValues()
    {
        // Arrange
        var dto = new CustomerContactCreateDto
        {
            FirstName = "Jeanette",
            LastName = "mikkelsen",
            Email = "jeanette.mikkelsen@example.com",
            CustomerId = 1
        };

        var expected = new CustomerContactEntity
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            CustomerId = dto.CustomerId
        };

        // Act
        var result = CustomerContactFactory.CreateCustomerContactEntity(dto);

        // Assert
        Assert.Equal(expected.FirstName, result.FirstName);
        Assert.Equal(expected.LastName, result.LastName);
        Assert.Equal(expected.Email, result.Email);
        Assert.Equal(expected.CustomerId, result.CustomerId);
    }

    [Fact]
    public void CreateCustomerContactModel_ShouldReturnModel_WithCorrectValues()
    {
        // Arrange
        var entity = new CustomerContactEntity
        {
            Id = 1,
            FirstName = "Hans",
            LastName = "Petersson",
            Email = "Hans.Pettersson@example.com",
            CustomerId = 2,
            Customer = new CustomerEntity { Id = 2, CustomerName = "Test Customer" }
        };

        // Act
        var result = CustomerContactFactory.CreateCustomerContactModel(entity);

        // Assert
        Assert.Equal(entity.Id, result.Id);
        Assert.Equal(entity.FirstName, result.FirstName);
        Assert.Equal(entity.LastName, result.LastName);
        Assert.Equal(entity.Email, result.Email);
        Assert.Equal(entity.CustomerId, result.CustomerId);
        Assert.Equal(entity.Customer.CustomerName, result.Customer.CustomerName);
    }
}
