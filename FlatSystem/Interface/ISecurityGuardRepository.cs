using FlatSystem.Dtos;
using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface ISecurityGuardRepository
    {
        Task<IEnumerable<SecurityGuardDto>> GetAllAsync();
        Task<SecurityGuardDto?> GetByIdAsync(int id);
        Task<CreateSecurityGuardDto?> GetByUserIdAsync(int userId);
        Task AddAsync(CreateSecurityGuardDto guard);
        Task UpdateAsync(SecurityGuardDto guard);
        Task DeleteAsync(int id);
    }
}
