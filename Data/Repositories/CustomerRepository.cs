using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : BaseRepository<CustomerEntity>(context), ICustomerRepository
{
    public async Task<CustomerEntity?> GetCustomerWithDetailsAsync(Expression<Func<CustomerEntity, bool>> predicate)
    {
        if (predicate == null)
        {
            throw new ArgumentNullException(nameof(predicate));
        }

        return await _context.Customers
            .Include(c => c.CustomerType)
            .Include(c => c.Contacts)
            .Include(c => c.Projects)
            .FirstOrDefaultAsync(predicate);
    }

    public async Task<ICollection<CustomerEntity>> GetCustomersWithDetailsAsync()
    {
        return await _context.Customers
            .Include(c => c.CustomerType)
            .Include(c => c.Contacts)
            .Include(c => c.Projects)
            .ToListAsync();
    }
}
