using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Data.Repositories;

public class ServiceRepository(DataContext context) : BaseRepository<ServiceEntity>(context), IServiceRepository
{
    public async Task<ServiceEntity?> GetServiceWithUnitAsync(Expression<Func<ServiceEntity, bool>> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var service = await _context.Services
                .Include(s => s.Unit)
                .FirstOrDefaultAsync(predicate);

            await transaction.CommitAsync();
            return service;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.WriteLine($"Fel vid hämtning av service med unit: {ex.Message}");
            return null;
        }
    }

    public async Task<ICollection<ServiceEntity>> GetServicesWithUnitAsync()
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var services = await _context.Services
                .Include(s => s.Unit)
                .ToListAsync();

            await transaction.CommitAsync();
            return services;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            Debug.WriteLine($"Fel vid hämtning av services med unit: {ex.Message}");
            return new List<ServiceEntity>();
        }
    }
}
