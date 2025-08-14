using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface ITenantRepository
    {
        Task<IEnumerable<Tenants>> GetByOwnerIdAsync(int ownerId);
        Task<Tenants?> GetByIdAsync(int id);
        Task AddAsync(Tenants tenant);
        Task UpdateAsync(Tenants tenant);
        Task DeleteAsync(int id);
    }
}
