using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Skapar en ny användare.
        /// Returnerar true om skapandet lyckas, annars false.
        /// </summary>
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

        /// <summary>
        /// Hämtar alla användare med detaljer.
        /// </summary>
        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            var entities = await _userRepository.GetUsersWithDetailsAsync();
            return entities.Select(e => UserFactory.CreateUserModel(e)).ToList();
        }

        /// <summary>
        /// Hämtar en användare med detaljer baserat på ID.
        /// </summary>
        public async Task<UserModel?> GetUserWithDetailsAsync(int id)
        {
            var entity = await _userRepository.GetUserWithDetailsAsync(u => u.Id == id);
            return entity != null ? UserFactory.CreateUserModel(entity) : null;
        }

        /// <summary>
        /// Uppdaterar en existerande användare.
        /// Returnerar true om uppdateringen lyckas, annars false.
        /// </summary>
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

        /// <summary>
        /// Tar bort en användare baserat på ID.
        /// Returnerar true om borttagningen lyckas, annars false.
        /// </summary>
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
