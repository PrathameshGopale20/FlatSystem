using FlatSystem.Dtos;
using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface IApartmentRepository
    {
        Task<IEnumerable<ApartmentDto>> GetAllAsync();
        Task<ApartmentDto?> GetByIdAsync(int id);
        Task AddAsync(CreateApartmentDto apartment);
        Task UpdateAsync(ApartmentDto apartment);
        Task DeleteAsync(int id);
    }
}
