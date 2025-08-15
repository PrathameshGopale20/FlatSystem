using FlatSystem.Dtos;
using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> GetByIdAsync(int id);
        Task<UserDto?> GetByUsernameAsync(string username);
        Task AddAsync(UserDto user);
        Task UpdateAsync(UserDto user);
        Task DeleteAsync(int id);
    }
}
