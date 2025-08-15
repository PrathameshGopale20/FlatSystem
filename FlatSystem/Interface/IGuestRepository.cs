using FlatSystem.Dtos;
using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface IGuestRepository
    {
        Task<IEnumerable<GuestDto>> GetByFlatIdAsync(int flatId);
        Task<GuestDto?> GetByIdAsync(int id);
        Task AddAsync(CreateGuestDto guest);
        Task UpdateAsync(GuestDto guest);
        Task DeleteAsync(int id);
    }
}
