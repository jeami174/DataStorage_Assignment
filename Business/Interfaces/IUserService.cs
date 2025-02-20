using Business.Dtos;
using Business.Models;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(UserCreateDto dto);
        Task<IEnumerable<UserModel>> GetAllUsersAsync();
        Task<UserModel?> GetUserWithDetailsAsync(int id);
        Task<bool> UpdateUserAsync(int id, UserUpdateDto dto);
        Task<bool> DeleteUserAsync(int id);
    }
}
