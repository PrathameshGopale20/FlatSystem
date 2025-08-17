using FlatSystem.Data;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class SocietyRepository: ISocietyRepository
    {
        private readonly AppDbContext _context;
        public SocietyRepository(AppDbContext context) { _context = context; }

        public async Task<IEnumerable<Society>> GetAllSocietiesAsync()
            => await _context.Societies.ToListAsync();

        public async Task<Society?> GetSocietyByIdAsync(int id)
            => await _context.Societies.FindAsync(id);

        public async Task<Society> AddSocietyAsync(Society society)
        {
            _context.Societies.Add(society);
            await _context.SaveChangesAsync();
            return society;
        }

        public async Task<Society?> UpdateSocietyAsync(int id, Society society)
        {
            var existing = await _context.Societies.FindAsync(id);
            if (existing == null) return null;
            existing.SocietyName = society.SocietyName;
            existing.Address = society.Address;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteSocietyAsync(int id)
        {
            var existing = await _context.Societies.FindAsync(id);
            if (existing == null) return false;
            _context.Societies.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
            => await _context.Societies.AnyAsync(s => s.Id == id);

        public async Task<Society?> FindByNameAsync(string name)
            => await _context.Societies.FirstOrDefaultAsync(s => s.SocietyName == name);
    }
}
