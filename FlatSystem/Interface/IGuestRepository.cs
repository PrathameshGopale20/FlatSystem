using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface IGuestRepository
    {
        Task<IEnumerable<Guest>> GetByFlatIdAsync(int flatId);
        Task<Guest?> GetByIdAsync(int id);
        Task AddAsync(Guest guest);
        Task UpdateAsync(Guest guest);
        Task DeleteAsync(int id);
    }
}
