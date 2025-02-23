using Business.Dtos;
using Business.Factories;
using Business.Models;
using Data.Entities;
using Xunit;

namespace Business.Tests.Factories;
    public class CustomerContactFactory_Tests
{
    [Fact]
    public void CreateCustomerContactEntity_ShouldReturnEntity_WithCorrectValues()
    {
        // Arrange
        var dto = new CustomerContactCreateDto
        {
            FirstName = "Martin",
            LastName = "Petersson",
            Email = "Martin.Petersson@example.com",
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
            FirstName = "Jessica",
            LastName = "Larsson",
            Email = "jessica.larsson@example.com",
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
