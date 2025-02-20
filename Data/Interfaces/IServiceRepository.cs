using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IServiceRepository : IBaseRepository<ServiceEntity>
    {
        Task<ServiceEntity?> GetServiceWithUnitAsync(Expression<Func<ServiceEntity, bool>> predicate);
        Task<ICollection<ServiceEntity>> GetServicesWithUnitAsync();
    }
}