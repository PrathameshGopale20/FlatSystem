using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface ISecurityGuardRepository
    {
        Task<IEnumerable<SecurityGuard>> GetAllAsync();
        Task<SecurityGuard?> GetByIdAsync(int id);
        Task<SecurityGuard?> GetByUserIdAsync(int userId);
        Task AddAsync(SecurityGuard guard);
        Task UpdateAsync(SecurityGuard guard);
        Task DeleteAsync(int id);
    }
}
