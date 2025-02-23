using Business.Models;

namespace Business.Interfaces
{
    public interface IStatusTypeService
    {
        void EnsureDefaultStatusTypes();
        Task<IEnumerable<StatusTypeModel>> GetAllStatusTypesAsync();
    }
}
