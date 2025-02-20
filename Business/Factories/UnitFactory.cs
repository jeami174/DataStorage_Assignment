using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class UnitFactory
{
    public static UnitModel CreateUnitModel(UnitEntity entity)
    {
        return new UnitModel
        {
            Id = entity.Id,
            UnitName = entity.UnitName
        };
    }
}
