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

        /// <summary>
        /// Hämtar alla StatusType.
        /// </summary>
        public async Task<IEnumerable<StatusTypeModel>> GetAllStatusTypesAsync()
        {
            var entities = await _statusTypeRepository.GetAllAsync();
            return entities.Select(e => StatusTypeFactory.CreateStatusTypeModel(e)).ToList();
        }
    }
}
