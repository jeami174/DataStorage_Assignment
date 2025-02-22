using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public class ProjectRepository : BaseRepository<ProjectEntity>, IProjectRepository
{
    public ProjectRepository(DataContext context) : base(context)
    {
    }

    private IQueryable<ProjectEntity> ProjectsWithDetails =>
        _context.Projects
            .Include(p => p.Customer)
                .ThenInclude(c => c.Contacts)
            .Include(p => p.Customer)
            .Include(p => p.Service)
                .ThenInclude(s => s.Unit)
            .Include(p => p.User)
                .ThenInclude(u => u.Role)
            .Include(p => p.Status);

    public async Task<ProjectEntity?> GetProjectWithDetailsAsync(int id)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var project = await ProjectsWithDetails.FirstOrDefaultAsync(p => p.Id == id);
            await transaction.CommitAsync();
            return project;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.WriteLine($"Fel vid hämtning av projekt med detaljer: {ex.Message}");
            return null;
        }
    }

    public async Task<ICollection<ProjectEntity>> GetAllProjectsWithDetailsAsync()
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var projects = await ProjectsWithDetails.ToListAsync();
            await transaction.CommitAsync();
            return projects;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.WriteLine($"Fel vid hämtning av alla projekt med detaljer: {ex.Message}");
            return new List<ProjectEntity>();
        }
    }
}




