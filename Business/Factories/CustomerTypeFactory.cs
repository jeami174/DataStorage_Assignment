using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerTypeFactory
{
    public static CustomerTypeModel CreateCustomerTypeModel(CustomerTypeEntity entity)
    {
        if (entity == null)
        {
            // TODO: Fix why CustomerTypeEntity is not fetched from database
            return new CustomerTypeModel()
            {
                Id = 0,
                CustomerTypeName = "Unknown"
            };
        }

        return new CustomerTypeModel()
        {
            Id = entity.Id,
            CustomerTypeName = entity.CustomerTypeName
        };
    }
}
