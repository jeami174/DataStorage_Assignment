using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services
{
    public class UnitService(IUnitRepository unitRepository) : IUnitService
    {
        private readonly IUnitRepository _unitRepository = unitRepository;

        public async Task<bool> CreateUnitAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }

            await _unitRepository.BeginTransactionAsync();

            try
            {
                await _unitRepository.CreateAsync(UnitFactory.CreateUnitEntity(name));
                await _unitRepository.SaveToDatabaseAsync();
                await _unitRepository.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _unitRepository.RollbackTransactionAsync();
                Debug.WriteLine($"Error creating user entity :: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<UnitModel>> GetAllUnitsAsync()
        {
            var entities = await _unitRepository.GetAllAsync();
            var units = entities.Select(UnitFactory.CreateUnitModel).ToList();
            return units;
        }
    }
}
