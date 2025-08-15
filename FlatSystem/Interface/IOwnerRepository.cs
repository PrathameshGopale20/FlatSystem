using FlatSystem.Dtos;
using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<OwnerDto>> GetAllAsync();
        Task<OwnerDto?> GetByIdAsync(int id);
        Task<CreateOwnerDto?> GetByUserIdAsync(int userId);
        Task AddAsync(CreateOwnerDto owner);
        Task UpdateAsync(OwnerDto owner);
        Task DeleteAsync(int id);
    }
}
