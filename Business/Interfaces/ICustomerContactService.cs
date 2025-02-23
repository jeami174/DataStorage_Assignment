using Business.Dtos;

namespace Business.Interfaces;

public interface ICustomerContactService
{
    Task<bool> CreateCustomerContactAsync(CustomerContactCreateDto dto);

    Task<bool> UpdateCustomerContactAsync(int id, CustomerContactUpdateDto dto);
}