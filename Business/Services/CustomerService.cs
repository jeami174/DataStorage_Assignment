using System.Diagnostics;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

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
        var entities = await _customerRepository.GetAllAsync();
        var customers = entities.Select(CustomerFactory.CreateCustomerModel).ToList();
        return customers;
    }

    public async Task<CustomerModel?> GetCustomerWithContactPersonsByIdAsync(int id)
    {
        var exists = await _customerRepository.ExistsAsync(x => x.Id == id);
        if (!exists)
        {
            return null;
        }

        var entity = await _customerRepository.GetOneWithDetailsAsync(
            query => query.Include(c => c.Contacts),
            x => x.Id == id);

        if (entity == null)
        {
            return null;
        }

        var customer = CustomerFactory.CreateCustomerModel(entity);
        return customer;
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
