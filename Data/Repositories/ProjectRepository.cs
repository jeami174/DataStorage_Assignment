using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Repositories;

public class ProjectRepository : BaseRepository<ProjectEntity>, IProjectRepository
{
    private readonly DataContext _context;
    public ProjectRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ProjectEntity?> GetProjectWithDetailsAsync(int id)
    {
        try
        {
            return await _context.Projects
                .Include(p => p.Customer)
                    .ThenInclude(c => c.Contacts)
                .Include(p => p.Customer)
                    .ThenInclude(c => c.CustomerType)
                .Include(p => p.Service)
                    .ThenInclude(s => s.Unit)
                .Include(p => p.User)
                    .ThenInclude(u => u.Role)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Fel vid hämtning av projekt med detaljer: {ex.Message}");
            return null;
        }
    }


    public async Task<ICollection<ProjectEntity>> GetAllProjectsWithDetailsAsync()
    {
        try
        {
            return await _context.Projects
                .Include(p => p.Customer)
                    .ThenInclude(c => c.Contacts)
                .Include(p => p.Customer)
                    .ThenInclude(c => c.CustomerType)
                .Include(p => p.Service)
                    .ThenInclude(s => s.Unit)
                .Include(p => p.User)
                    .ThenInclude(u => u.Role)
                .Include(p => p.Status)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Fel vid hämtning av alla projekt med detaljer: {ex.Message}");
            return new List<ProjectEntity>();
        }
    }
}




