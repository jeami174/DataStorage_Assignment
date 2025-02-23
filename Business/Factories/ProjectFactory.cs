using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectEntity CreateProjectEntity(ProjectCreateDto dto, decimal servicePricePerUnit)
    {
        return new ProjectEntity
        {
            Title = dto.Title,
            Description = dto.Description,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            QuantityofServiceUnits = dto.QuantityofServiceUnits,
            CustomerId = dto.CustomerId,
            ServiceId = dto.ServiceId,
            StatusId = dto.StatusId,
            UserId = dto.UserId,
            TotalPrice = servicePricePerUnit * dto.QuantityofServiceUnits
        };
    }

    public static void UpdateProjectEntity(ProjectEntity entity, ProjectUpdateDto dto)
    {
        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.StartDate = dto.StartDate;
        entity.EndDate = dto.EndDate;
        entity.QuantityofServiceUnits = dto.QuantityofServiceUnits;
        entity.TotalPrice = entity.Service.PricePerUnit * dto.QuantityofServiceUnits;
        entity.StatusId = dto.StatusId;
    }

    public static ProjectUpdateDto CreateUpdateDto(ProjectModel model)
    {
        return new ProjectUpdateDto
        {
            Title = model.Title,
            Description = model.Description,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            QuantityofServiceUnits = model.QuantityofServiceUnits,
            StatusId= model.Status.Id
        };
    }

    public static ProjectModel CreateProjectModel(ProjectEntity entity)
    {
        return new ProjectModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            QuantityofServiceUnits = entity.QuantityofServiceUnits,
            TotalPrice = entity.QuantityofServiceUnits * entity.Service.PricePerUnit,
            Customer = CustomerFactory.CreateCustomerModel(entity.Customer),
            Service = ServiceFactory.CreateServiceModel(entity.Service),
            Status = StatusTypeFactory.CreateStatusTypeModel(entity.Status),
            User = UserFactory.Create(entity.User)
        };
    }
}



