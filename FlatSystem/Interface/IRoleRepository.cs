using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Roles>> GetAllAsync();
        Task<Roles?> GetByIdAsync(int id);
        Task<Roles?> GetByNameAsync(string roleName);
        Task AddAsync(Roles role);
        Task UpdateAsync(Roles role);
        Task DeleteAsync(int id);
    }
}
