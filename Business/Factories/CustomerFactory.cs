using System;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{

    public static CustomerCreateDto Create() => new();

    public static CustomerEntity CreateCustomerEntity(CustomerCreateDto dto) => new()
    {
        CustomerName = dto.CustomerName,
        CustomerTypeId = dto.CustomerTypeId
    };

    public static CustomerModel CreateCustomerModel(CustomerEntity entity)
    {
        var customerContacts = new List<CustomerContactModel>();

        foreach (var row in entity.Contacts)
            customerContacts.Add(new CustomerContactModel
            {
                FirstName = row.FirstName,
                LastName = row.LastName,
                Email = row.Email,
                CustomerId = row.CustomerId,
            });

        return new CustomerModel()
        {
            Id = entity.Id,
            CustomerName = entity.CustomerName,
            CustomerType = CustomerTypeFactory.CreateCustomerTypeModel(entity.CustomerType),
            CustomerContacts = customerContacts
        };
    }

    public static CustomerEntity CreateUpdatedEntity(CustomerUpdateDto dto, CustomerEntity entity)
    {
        entity.CustomerName = dto.CustomerName;
        entity.CustomerTypeId = dto.CustomerTypeId;
        return entity;
    }

}
