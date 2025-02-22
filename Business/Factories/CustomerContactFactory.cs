using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class CustomerContactFactory
{
    public static CustomerContactCreateDto CreateCustomerContactDto()
    {
        return new CustomerContactCreateDto();
    }

    public static CustomerContactEntity CreateCustomerContactEntity(CustomerContactCreateDto customerContactCreateDto)
    {
        return new CustomerContactEntity
        {
            FirstName = customerContactCreateDto.FirstName,
            LastName = customerContactCreateDto.LastName,
            Email = customerContactCreateDto.Email,
            CustomerId = customerContactCreateDto.CustomerId
        };
    }

    public static CustomerContactModel CreateCustomerContactModel(CustomerContactEntity customerContactEntity)
    {
        return new CustomerContactModel()
        {
            Id = customerContactEntity.Id,
            FirstName = customerContactEntity.FirstName,
            LastName = customerContactEntity.LastName,
            Email = customerContactEntity.Email,
            
            CustomerId = customerContactEntity.CustomerId,
            Customer = CustomerFactory.CreateCustomerModel(customerContactEntity.Customer)
        };
    }

    public static CustomerContactEntity UpdateCustomerContactEntity(CustomerContactEntity entity, CustomerContactUpdateDto dto)
    {
        return new CustomerContactEntity
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
        };
    }
}
