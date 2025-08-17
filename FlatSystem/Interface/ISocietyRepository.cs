using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface ISocietyRepository
    {
        Task<IEnumerable<Society>> GetAllSocietiesAsync();
        Task<Society?> GetSocietyByIdAsync(int id);
        Task<Society> AddSocietyAsync(Society society);
        Task<Society?> UpdateSocietyAsync(int id, Society society);
        Task<bool> DeleteSocietyAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<Society?> FindByNameAsync(string name);
    }
}
