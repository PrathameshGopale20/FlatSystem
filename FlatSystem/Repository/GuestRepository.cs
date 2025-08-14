using FlatSystem.Data;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class GuestRepository : IGuestRepository
    {
        private readonly AppDbContext _context;
        public GuestRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Guest>> GetByFlatIdAsync(int flatId) =>
            await _context.Guests.Where(g => g.VisitingFlatId == flatId).ToListAsync();

        public async Task<Guest?> GetByIdAsync(int id) =>
            await _context.Guests.FirstOrDefaultAsync(g => g.Id == id);

        public async Task AddAsync(Guest guest)
        {
            _context.Guests.Add(guest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guest guest)
        {
            _context.Guests.Update(guest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var guest = await GetByIdAsync(id);
            if (guest != null)
            {
                _context.Guests.Remove(guest);
                await _context.SaveChangesAsync();
            }
        }
    }
}
