using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface IApartmentRepository
    {
        Task<IEnumerable<Apartments>> GetAllAsync();
        Task<Apartments?> GetByIdAsync(int id);
        Task AddAsync(Apartments apartment);
        Task UpdateAsync(Apartments apartment);
        Task DeleteAsync(int id);
    }
}
