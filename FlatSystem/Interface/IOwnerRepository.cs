using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owners>> GetAllAsync();
        Task<Owners?> GetByIdAsync(int id);
        Task<Owners?> GetByUserIdAsync(int userId);
        Task AddAsync(Owners owner);
        Task UpdateAsync(Owners owner);
        Task DeleteAsync(int id);
    }
}
