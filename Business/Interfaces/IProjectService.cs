using Business.Dtos;
using Business.Models;

namespace Business.Interfaces
{
    public interface IProjectService
    {

        Task<bool> CreateProjectAsync(ProjectCreateDto dto);
        Task<bool> UpdateProjectAsync(int id, ProjectUpdateDto dto);
        Task<bool> DeleteProjectAsync(int id);
        Task<IEnumerable<ProjectModel>> GetAllProjectsWithDetailsAsync();
        Task<ProjectModel?> GetProjectWithDetailsByIdAsync(int id);
    }
}
