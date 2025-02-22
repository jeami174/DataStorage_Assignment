using Business.Dtos;
using Business.Models;

namespace Business.Interfaces
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomerAsync(CustomerCreateDto dto);
        Task<IEnumerable<CustomerModel>> GetAllCustomersAsync();
        Task<CustomerModel?> GetCustomerWithContactPersonsByIdAsync(int id);
        Task<bool> UpdateCustomerAsync(int id, CustomerUpdateDto dto);
        Task<bool> DeleteCustomerAsync(int id);
    }
}

