using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        Task<UserEntity?> GetUserWithDetailsAsync(Expression<Func<UserEntity, bool>> predicate);
        Task<ICollection<UserEntity>> GetUsersWithDetailsAsync();
        Task<ICollection<UserEntity>> GetUsersWithDetailsAsync(Expression<Func<UserEntity, bool>> predicate);
    }
}