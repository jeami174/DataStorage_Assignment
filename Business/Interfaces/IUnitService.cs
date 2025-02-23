using Business.Models;

namespace Business.Interfaces
{
    public interface IUnitService
    {
        Task<bool> CreateUnitAsync(string name);
        Task<IEnumerable<UnitModel>> GetAllUnitsAsync();
    }
}
