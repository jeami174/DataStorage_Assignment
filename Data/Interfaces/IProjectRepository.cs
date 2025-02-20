using Data.Entities;

namespace Data.Interfaces
{
    public interface IProjectRepository : IBaseRepository<ProjectEntity>
    {
        Task<ProjectEntity?> GetProjectWithDetailsAsync(int id);
        Task<ICollection<ProjectEntity>> GetAllProjectsWithDetailsAsync();
    }
}