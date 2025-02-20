using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Business.Services
{
    public class CustomerContactService : ICustomerContactService
    {
        private readonly ICustomerContactRepository _customerContactRepository;

        public CustomerContactService(ICustomerContactRepository customerContactRepository)
        {
            _customerContactRepository = customerContactRepository;
        }

        /// <summary>
        /// Skapar en ny CustomerContact.
        /// Returnerar true om skapandet lyckas, annars false.
        /// </summary>
        public async Task<bool> CreateCustomerContactAsync(CustomerContactCreateDto dto)
        {
            if (dto == null)
                return false;

            if (string.IsNullOrWhiteSpace(dto.FirstName) ||
                string.IsNullOrWhiteSpace(dto.LastName) ||
                string.IsNullOrWhiteSpace(dto.Email))
            {
                return false;
            }

            var exists = await _customerContactRepository.ExistsAsync(x => x.Email.ToLower() == dto.Email.ToLower());
            if (exists)
            {
                return false;
            }

            await _customerContactRepository.BeginTransactionAsync();

            try
            {
                await _customerContactRepository.CreateAsync(CustomerContactFactory.CreateCustomerContactEntity(dto));
                await _customerContactRepository.SaveToDatabaseAsync();
                await _customerContactRepository.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _customerContactRepository.RollbackTransactionAsync();
                Debug.WriteLine($"Error creating CustomerContact: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Hämtar alla CustomerContacts med tillhörande kundinformation.
        /// </summary>
        public async Task<IEnumerable<CustomerContactModel>> GetAllCustomerContactsAsync()
        {
            try
            {
                var entities = await _customerContactRepository.GetAllWithDetailsAsync(query =>
                    query.Include(cc => cc.Customer));
                return entities.Select(e => CustomerContactFactory.CreateCustomerContactModel(e)).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting all CustomerContacts: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Hämtar en CustomerContact baserat på ID.
        /// </summary>
        public async Task<CustomerContactModel?> GetCustomerContactByIdAsync(int id)
        {
            try
            {
                var exists = await _customerContactRepository.ExistsAsync(x => x.Id == id);
                if (!exists)
                    return null;

                var entity = await _customerContactRepository.GetOneAsync(x => x.Id == id);
                return entity != null ? CustomerContactFactory.CreateCustomerContactModel(entity) : null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting CustomerContact by id: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Uppdaterar en existerande CustomerContact.
        /// Returnerar true om uppdateringen lyckas, annars false.
        /// </summary>
        public async Task<bool> UpdateCustomerContactAsync(int id, CustomerContactUpdateDto dto)
        {
            if (dto == null)
                return false;

            if (string.IsNullOrWhiteSpace(dto.FirstName) ||
                string.IsNullOrWhiteSpace(dto.LastName) ||
                string.IsNullOrWhiteSpace(dto.Email))
            {
                return false;
            }

            var entity = await _customerContactRepository.GetOneAsync(cc => cc.Id == id);
            if (entity == null)
                return false;

            CustomerContactFactory.UpdateCustomerContactEntity(entity, dto);

            await _customerContactRepository.BeginTransactionAsync();

            try
            {
                _customerContactRepository.Update(entity);
                await _customerContactRepository.SaveToDatabaseAsync();
                await _customerContactRepository.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _customerContactRepository.RollbackTransactionAsync();
                Debug.WriteLine($"Error updating CustomerContact: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Tar bort en CustomerContact baserat på ID.
        /// Returnerar true om borttagningen lyckas, annars false.
        /// </summary>
        public async Task<bool> DeleteCustomerContactAsync(int id)
        {
            var entity = await _customerContactRepository.GetOneAsync(cc => cc.Id == id);
            if (entity == null)
                return false;

            await _customerContactRepository.BeginTransactionAsync();

            try
            {
                _customerContactRepository.Delete(entity);

                await _customerContactRepository.SaveToDatabaseAsync();
                await _customerContactRepository.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _customerContactRepository.RollbackTransactionAsync();
                Debug.WriteLine($"Error deleting CustomerContact: {ex.Message}");
                return false;
            }
        }
    }
}

