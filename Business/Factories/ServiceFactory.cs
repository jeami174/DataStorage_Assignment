using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class ServiceFactory
{
    public static ServiceCreateDto Create()
    {
        return new ServiceCreateDto();
    }
    public static ServiceEntity CreateServiceEntity(ServiceCreateDto dto)
    {
        return new ServiceEntity
        {
            ServiceName = dto.ServiceName,
            PricePerUnit = dto.PricePerUnit,
            UnitId = dto.UnitId
        };
    }
    public static ServiceModel CreateServiceModel(ServiceEntity entity)
    {
        return new ServiceModel
        {
            Id = entity.Id,
            ServiceName = entity.ServiceName,
            PricePerUnit = entity.PricePerUnit,
            Unit = UnitFactory.CreateUnitModel(entity.Unit)
        };
    }

    public static ServiceEntity UpdateServiceEntity(ServiceEntity entity, ServiceUpdateDto dto)
    {
        entity.ServiceName = dto.ServiceName;
        entity.PricePerUnit = dto.PricePerUnit;
        entity.UnitId = dto.UnitId;
        return entity;
    }
}

