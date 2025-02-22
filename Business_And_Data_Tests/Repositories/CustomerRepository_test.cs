using Data.Contexts;
using Data.Interfaces;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Business_And_Data_Tests.Repositories;

public class CustomerRepository_test
{
    private readonly DataContext _context;
    private readonly ICustomerRepository _customerRepository;

    public CustomerRepository_test()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase($"{Guid.NewGuid}")
            .Options;
        _context = new DataContext(options);
        _customerRepository = new CustomerRepository(_context);
    }
}
