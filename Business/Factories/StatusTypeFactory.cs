using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class StatusTypeFactory
{
    public static StatusTypeModel CreateStatusTypeModel(StatusTypeEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        return new StatusTypeModel
        {
            Id = entity.Id,
            StatusTypeName = entity.StatusTypeName
        };
    }
}
