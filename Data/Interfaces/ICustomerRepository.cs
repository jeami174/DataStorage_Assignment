using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface ICustomerRepository : IBaseRepository<CustomerEntity>
{
}