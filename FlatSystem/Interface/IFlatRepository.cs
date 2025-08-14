using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface IFlatRepository
    {
        Task<IEnumerable<Flats>> GetAllAsync();
        Task<IEnumerable<Flats>> GetByApartmentIdAsync(int apartmentId);
        Task<Flats?> GetByIdAsync(int id);
        Task AddAsync(Flats flat);
        Task UpdateAsync(Flats flat);
        Task DeleteAsync(int id);
    }
}
