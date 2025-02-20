using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Interfaces;
using System.Diagnostics;

namespace Business.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// Skapar en ny user role. Returnerar true om skapandet lyckas, annars false.
        /// </summary>
        public async Task<bool> CreateAsync(UserRoleCreateDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.RoleName))
                return false;

            // Kontrollera om en roll med samma namn redan finns
            var exists = await _userRoleRepository.ExistsAsync(x => x.RoleName.ToLower() == dto.RoleName.ToLower());
            if (exists)
            {
                return false;
            }

            await _userRoleRepository.BeginTransactionAsync();

            try
            {
                await _userRoleRepository.CreateAsync(UserRoleFactory.CreateUserRoleEntity(dto));
                await _userRoleRepository.SaveToDatabaseAsync();
                await _userRoleRepository.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _userRoleRepository.RollbackTransactionAsync();
                Debug.WriteLine($"Error creating user role entity :: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Hämtar alla user roles.
        /// </summary>
        public async Task<IEnumerable<UserRoleModel>> GetAllUserRolesAsync()
        {
            var entities = await _userRoleRepository.GetAllAsync();
            return entities.Select(e => UserRoleFactory.CreateUserRoleModel(e)).ToList();
        }

        /// <summary>
        /// Hämtar en user role baserat på ID.
        /// </summary>
        public async Task<UserRoleModel?> GetUserRoleByIdAsync(int id)
        {
            var exists = await _userRoleRepository.ExistsAsync(x => x.Id == id);
            if (!exists)
                return null;

            var entity = await _userRoleRepository.GetOneAsync(x => x.Id == id);
            return entity != null ? UserRoleFactory.CreateUserRoleModel(entity) : null;
        }

        /// <summary>
        /// Uppdaterar en existerande user role. Returnerar true om uppdateringen lyckas, annars false.
        /// </summary>
        public async Task<bool> UpdateAsync(int id, UserRoleUpdateDto dto)
        {
            if (dto == null)
                return false;

            var entity = await _userRoleRepository.GetOneAsync(x => x.Id == id);
            if (entity == null)
                return false;

            // Uppdatera entiteten med hjälp av fabriken
            UserRoleFactory.UpdateUserRoleEntity(entity, dto);

            await _userRoleRepository.BeginTransactionAsync();

            try
            {
                _userRoleRepository.Update(entity);
                await _userRoleRepository.SaveToDatabaseAsync();
                await _userRoleRepository.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _userRoleRepository.RollbackTransactionAsync();
                Debug.WriteLine($"Error updating user role entity :: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Tar bort en user role baserat på ID. Returnerar true om borttagningen lyckas, annars false.
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _userRoleRepository.GetOneAsync(x => x.Id == id);
            if (entity == null)
                return false;

            await _userRoleRepository.BeginTransactionAsync();

            try
            {
                _userRoleRepository.Delete(entity);
                await _userRoleRepository.SaveToDatabaseAsync();
                await _userRoleRepository.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                await _userRoleRepository.RollbackTransactionAsync();
                Debug.WriteLine($"Error deleting user role entity :: {ex.Message}");
                return false;
            }
        }
    }
}
