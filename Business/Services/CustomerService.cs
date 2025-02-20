using System.Diagnostics;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> CreateCustomerAsync(CustomerCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.CustomerName))
                return false;

            dto.CustomerName = dto.CustomerName.ToLower();

            bool exists = await _customerRepository.ExistsAsync(x => x.CustomerName.ToLower() == dto.CustomerName);
            if (exists)
                return false;

            await _customerRepository.BeginTransactionAsync();

            try
            {
                await _customerRepository.CreateAsync(CustomerFactory.CreateCustomerEntity(dto));
                await _customerRepository.SaveToDatabaseAsync();
                await _customerRepository.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _customerRepository.RollbackTransactionAsync();
                Debug.WriteLine($"Error creating customer entity :: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<CustomerModel>> GetAllCustomersAsync()
        {
            var entities = await _customerRepository.GetCustomersWithDetailsAsync();
            return entities.Select(e => CustomerFactory.CreateCustomerModel(e));
        }

        public async Task<CustomerModel?> GetCustomerWithDetailsAsync(int id)
        {
            var entity = await _customerRepository.GetCustomerWithDetailsAsync(c => c.Id == id);
            return entity != null ? CustomerFactory.CreateCustomerModel(entity) : null;
        }

        public async Task<bool> UpdateCustomerAsync(int id, CustomerUpdateDto dto)
        {
            var entity = await _customerRepository.GetOneAsync(c => c.Id == id);
            if (entity == null)
                return false;

            CustomerFactory.CreateUpdatedEntity(dto, entity);

            await _customerRepository.BeginTransactionAsync();

            try
            {
                _customerRepository.Update(entity);
                await _customerRepository.SaveToDatabaseAsync();
                await _customerRepository.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _customerRepository.RollbackTransactionAsync();
                Debug.WriteLine($"Error updating customer entity :: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            var entity = await _customerRepository.GetOneAsync(c => c.Id == id);
            if (entity == null)
                return false;

            await _customerRepository.BeginTransactionAsync();

            try
            {
                _customerRepository.Delete(entity);
                await _customerRepository.SaveToDatabaseAsync();
                await _customerRepository.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _customerRepository.RollbackTransactionAsync();
                Debug.WriteLine($"Error deleting customer entity :: {ex.Message}");
                return false;
            }
        }
    }
}
