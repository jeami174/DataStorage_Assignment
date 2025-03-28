﻿using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ServiceFactory
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
        var projects = new List<ProjectModel>();

        return new ServiceModel()
        {
            Id = entity.Id,
            ServiceName = entity.ServiceName,
            PricePerUnit = entity.PricePerUnit,
            UnitId = entity.UnitId,
            Unit = UnitFactory.CreateUnitModel(entity.Unit),
            Projects = projects
        };
    }

    public static void UpdateServiceEntity(ServiceEntity entity, ServiceUpdateDto dto)
    {
        entity.ServiceName = dto.ServiceName;
        entity.PricePerUnit = dto.PricePerUnit;
    }
}

