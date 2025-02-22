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
    };

    public static CustomerModel CreateCustomerModel(CustomerEntity entity)
    {
        var customerContacts = new List<CustomerContactModel>();

        foreach (var row in entity.Contacts ?? Enumerable.Empty<CustomerContactEntity>())
        {
            customerContacts.Add(new CustomerContactModel
            {
                FirstName = row.FirstName,
                LastName = row.LastName,
                Email = row.Email,
                CustomerId = row.CustomerId,
            });
        }

        return new CustomerModel()
        {
            Id = entity.Id,
            CustomerName = entity.CustomerName,
            CustomerContacts = customerContacts
        };
    }

    public static CustomerEntity CreateUpdatedEntity(CustomerUpdateDto dto, CustomerEntity entity)
    {
        entity.CustomerName = dto.CustomerName;
        return entity;
    }

}
