using Business.Dtos;
using Business.Models;

namespace Business.Interfaces
{
    public interface IUserRoleService
    {
        Task<bool> CreateAsync(UserRoleCreateDto dto);
        Task<IEnumerable<UserRoleModel>> GetAllUserRolesAsync();
        Task<UserRoleModel?> GetUserRoleByIdAsync(int id);
        Task<bool> UpdateAsync(int id, UserRoleUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
