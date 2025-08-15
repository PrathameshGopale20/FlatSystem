using FlatSystem.Dtos;
using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface IRoleRepository
    {
        Task<IEnumerable<RoleDto>> GetAllAsync();
        Task<RoleDto?> GetByIdAsync(int id);
        Task<RoleDto?> GetByNameAsync(string roleName);
        Task AddAsync(RoleDto role);
        Task UpdateAsync(RoleDto role);
        Task DeleteAsync(int id);
    }
}
