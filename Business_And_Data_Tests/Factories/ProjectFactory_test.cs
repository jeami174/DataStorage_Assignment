using Business.Dtos;
using Business.Factories;
using Data.Entities;

namespace Business.Tests.Factories;
public class ProjectFactory_Tests 
{
    [Fact]
    public void CreateProjectEntity_ShouldReturnEntity_WithCorrectValues()
    {
        // Arrange
        var dto = new ProjectCreateDto
        {
            Title = "New Website",
            Description = "Develop a new e-commerce website",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(3),
            CustomerId = 1,
            ServiceId = 2,
            StatusId = 3,
            UserId = 4
        };

        // Act
        var result = ProjectFactory.CreateProjectEntity(dto, 0);

        // Assert
        Assert.Equal(dto.Title, result.Title);
        Assert.Equal(dto.Description, result.Description);
        Assert.Equal(dto.StartDate, result.StartDate);
        Assert.Equal(dto.EndDate, result.EndDate);
        Assert.Equal(dto.CustomerId, result.CustomerId);
        Assert.Equal(dto.ServiceId, result.ServiceId);
        Assert.Equal(dto.StatusId, result.StatusId);
        Assert.Equal(dto.UserId, result.UserId);
        Assert.Equal(0, result.TotalPrice);
    }

    [Fact]
    public void CreateProjectModel_ShouldReturnModel_WithCorrectValues()
    {
        // Arrange
        var entity = new ProjectEntity
        {
            Id = 1,
            Title = "Mobile App",
            Description = "Develop a mobile app for iOS and Android",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(5),
            Customer = new CustomerEntity
            {
                Id = 1,
                CustomerName = "TechCorp",
                Contacts = new List<CustomerContactEntity>(),
            },
            Service = new ServiceEntity
            {
                Id = 2,
                ServiceName = "App Development",
                PricePerUnit = 100,
                Unit = new UnitEntity { Id = 1, UnitName = "Hours" }
            },
            Status = new StatusTypeEntity
            {
                Id = 3,
                StatusTypeName = "In Progress"
            },
            User = new UserEntity
            {
                Id = 4,
                FirstName = "DeveloperX",
                LastName = "Doe",
                Email = "developerx@example.com",
                RoleId = 23,
                Role = new UserRoleEntity { Id = 23, RoleName = "Admin" }
            }
        };

        // Act
        var result = ProjectFactory.CreateProjectModel(entity);

        // Assert
        Assert.Equal(entity.Id, result.Id);
        Assert.Equal(entity.Title, result.Title);
        Assert.Equal(entity.Description, result.Description);
        Assert.Equal(entity.StartDate, result.StartDate);
        Assert.Equal(entity.EndDate, result.EndDate);
        Assert.Equal(entity.Customer.CustomerName, result.Customer.CustomerName);
        Assert.Equal(entity.Service.ServiceName, result.Service.ServiceName);
        Assert.Equal(entity.Status.StatusTypeName, result.Status.StatusTypeName);
        Assert.Equal(entity.User.FirstName, result.User.FirstName);
    }
}
