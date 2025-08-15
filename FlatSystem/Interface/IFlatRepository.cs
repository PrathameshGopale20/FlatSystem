using FlatSystem.Dtos;
using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface IFlatRepository
    {
        Task<IEnumerable<FlatDto>> GetAllAsync();
        Task<IEnumerable<CreateFlatDto>> GetByApartmentIdAsync(int apartmentId);
        Task<FlatDto?> GetByIdAsync(int id);
        Task AddAsync(CreateFlatDto flat);
        Task UpdateAsync(FlatDto flat);
        Task DeleteAsync(int id);
    }
}
