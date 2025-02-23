using System.Diagnostics;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;

namespace Business.Services
{
    public class StatusTypeService : IStatusTypeService
    {
        private readonly IStatusTypeRepository _statusTypeRepository;

        public StatusTypeService(IStatusTypeRepository statusTypeRepository)
        {
            _statusTypeRepository = statusTypeRepository;
        }

        public void EnsureDefaultStatusTypes()
        {
            // Skapa standard status typer om de inte finns
            List<string> requiredStatuses = new List<string> { "Not started", "Started", "Completed" };
            foreach (var status in requiredStatuses)
            {
                if (!_statusTypeRepository.ExistsAsync(e => e.StatusTypeName == status).Result)
                {
                    try
                    {
                        _statusTypeRepository.CreateAsync(StatusTypeFactory.CreateStatusTypeEntity(status));
                        _statusTypeRepository.SaveToDatabaseAsync();
                        _statusTypeRepository.CommitTransactionAsync();
                    }
                    catch (Exception ex)
                    {
                        _statusTypeRepository.RollbackTransactionAsync();
                        Debug.WriteLine($"Error creating status type entity :: {ex.Message}");
                    }
                }
            }
        }

        public async Task<IEnumerable<StatusTypeModel>> GetAllStatusTypesAsync()
        {
            var entities = await _statusTypeRepository.GetAllAsync();
            return entities.Select(e => StatusTypeFactory.CreateStatusTypeModel(e)).ToList();
        }
    }
}
