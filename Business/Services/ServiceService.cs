using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Business.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        /// <summary>
        /// Skapar en ny service. Returnerar true om skapandet lyckas, annars false.
        /// </summary>
        public async Task<bool> CreateAsync(ServiceCreateDto dto)
        {
            if (dto == null) return false;
            if (string.IsNullOrWhiteSpace(dto.ServiceName))
                return false;

            // Normalisera servicens namn
            dto.ServiceName = dto.ServiceName.Trim().ToLower();

            // Kontrollera om en service med samma namn redan finns
            var exists = await _serviceRepository.ExistsAsync(x => x.ServiceName.ToLower() == dto.ServiceName);
            if (exists)
            {
                return false;
            }

            await _serviceRepository.BeginTransactionAsync();

            try
            {
                // Skapa entiteten och spara den
                await _serviceRepository.CreateAsync(ServiceFactory.CreateServiceEntity(dto));
                await _serviceRepository.SaveToDatabaseAsync();
                await _serviceRepository.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _serviceRepository.RollbackTransactionAsync();
                Debug.WriteLine($"Error creating service entity :: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Hämtar alla services med tillhörande Unit.
        /// </summary>
        public async Task<IEnumerable<ServiceModel>> GetAllServicesAsync()
        {
            try
            {
                var entities = await _serviceRepository.GetAllWithDetailsAsync(query => query.Include(s => s.Unit));
                return entities.Select(e => ServiceFactory.CreateServiceModel(e)).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving services: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Hämtar en service med tillhörande Unit baserat på ID.
        /// </summary>
        public async Task<ServiceModel?> GetServiceWithUnitAsync(int id)
        {
            try
            {
                var entity = await _serviceRepository.GetOneWithDetailsAsync(
                    query => query.Include(s => s.Unit),
                    s => s.Id == id);
                return entity != null ? ServiceFactory.CreateServiceModel(entity) : null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error retrieving service by id: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Uppdaterar en existerande service. Returnerar true om uppdateringen lyckas, annars false.
        /// </summary>
        public async Task<bool> UpdateAsync(int id, ServiceUpdateDto dto)
        {
            if (dto == null) return false;
            if (string.IsNullOrWhiteSpace(dto.ServiceName))
                return false;

            try
            {
                var entity = await _serviceRepository.GetOneAsync(s => s.Id == id);
                if (entity == null)
                    return false;

                // Uppdatera entiteten med hjälp av fabriken
                ServiceFactory.UpdateServiceEntity(entity, dto);

                await _serviceRepository.BeginTransactionAsync();

                try
                {
                    // Använder den synkrona Update-metoden (från BaseRepository)
                    _serviceRepository.Update(entity);
                    await _serviceRepository.SaveToDatabaseAsync();
                    await _serviceRepository.CommitTransactionAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await _serviceRepository.RollbackTransactionAsync();
                    Debug.WriteLine($"Error updating service entity :: {ex.Message}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in update service: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Tar bort en service baserat på ID. Returnerar true om borttagningen lyckas, annars false.
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _serviceRepository.GetOneAsync(s => s.Id == id);
                if (entity == null)
                    return false;

                await _serviceRepository.BeginTransactionAsync();

                try
                {
                    // Använder den synkrona Delete-metoden
                    _serviceRepository.Delete(entity);
                    await _serviceRepository.SaveToDatabaseAsync();
                    await _serviceRepository.CommitTransactionAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await _serviceRepository.RollbackTransactionAsync();
                    Debug.WriteLine($"Error deleting service entity :: {ex.Message}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in delete service: {ex.Message}");
                return false;
            }
        }
    }
}

