using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Business.Services
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<bool> CreateUserAsync(UserCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FirstName) ||
                string.IsNullOrWhiteSpace(dto.LastName) ||
                string.IsNullOrWhiteSpace(dto.Email))
            {
                return false;
            }

            dto.Email = dto.Email.ToLower();

            bool exists = await _userRepository.ExistsAsync(u => u.Email.ToLower() == dto.Email);
            if (exists)
            {
                return false;
            }

            await _userRepository.BeginTransactionAsync();

            try
            {
                await _userRepository.CreateAsync(UserFactory.CreateUserEntity(dto));
                await _userRepository.SaveToDatabaseAsync();
                await _userRepository.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _userRepository.RollbackTransactionAsync();
                Debug.WriteLine($"Error creating user entity :: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            var entities = await _userRepository.GetAllAsync();
            var users = entities.Select(UserFactory.Create).ToList();
            return users;
        }

        public async Task<UserModel?> GetEmployeeWithDetailsByIdAsync(int id)
        {
            var exists = await _userRepository.ExistsAsync(x => x.Id == id);
            if (!exists)
            {
                return null;
            }

            var entity = await _userRepository.GetOneWithDetailsAsync(query => query.Include(e => e.Role), x => x.Id == id);
            if (entity == null)
            {
                return null;
            }

            var user = UserFactory.Create(entity);
            return user;
        }



        public async Task<bool> UpdateUserAsync(int id, UserUpdateDto dto)
        {
            if (dto == null)
            {
                return false;
            }

            dto.Email = dto.Email.ToLower();

            var entity = await _userRepository.GetOneAsync(u => u.Id == id);
            if (entity == null)
            {
                return false;
            }

            UserFactory.UpdateUserEntity(entity, dto);

            await _userRepository.BeginTransactionAsync();

            try
            {
                _userRepository.Update(entity);
                await _userRepository.SaveToDatabaseAsync();
                await _userRepository.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _userRepository.RollbackTransactionAsync();
                Debug.WriteLine($"Error updating user entity :: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var entity = await _userRepository.GetOneAsync(u => u.Id == id);
            if (entity == null)
            {
                return false;
            }

            await _userRepository.BeginTransactionAsync();

            try
            {
                _userRepository.Delete(entity);
                await _userRepository.SaveToDatabaseAsync();
                await _userRepository.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _userRepository.RollbackTransactionAsync();
                Debug.WriteLine($"Error deleting user entity :: {ex.Message}");
                return false;
            }
        }
    }
}
