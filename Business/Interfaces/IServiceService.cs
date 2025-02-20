using Business.Dtos;
using Business.Models;

namespace Business.Interfaces
{
    public interface IServiceService
    {
        Task<bool> CreateAsync(ServiceCreateDto dto);
        Task<IEnumerable<ServiceModel>> GetAllServicesAsync();
        Task<ServiceModel?> GetServiceWithUnitAsync(int id);
        Task<bool> UpdateAsync(int id, ServiceUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}

