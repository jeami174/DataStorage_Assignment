using Business.Models;
using Data.Entities;
using Microsoft.IdentityModel.Tokens;

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

    public static StatusTypeEntity CreateStatusTypeEntity(string name)
    {
        if (name.IsNullOrEmpty())
            throw new ArgumentNullException();

        return new StatusTypeEntity
        {
            StatusTypeName = name
        };
    }
}
