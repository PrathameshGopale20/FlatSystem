using FlatSystem.Dtos;
using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface ITenantRepository
    {
        Task<IEnumerable<CreateTenantDto>> GetByOwnerIdAsync(int ownerId);
        Task<TenantDto?> GetByIdAsync(int id);
        Task AddAsync(CreateTenantDto tenant);
        Task UpdateAsync(TenantDto tenant);
        Task DeleteAsync(int id);
    }
}
