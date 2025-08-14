using FlatSystem.Data;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly AppDbContext _context;
        public OwnerRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Owners>> GetAllAsync() =>
            await _context.Owners.Include(o => o.Flat).Include(o => o.Tenants).ToListAsync();

        public async Task<Owners?> GetByIdAsync(int id) =>
            await _context.Owners.Include(o => o.Flat).Include(o => o.Tenants)
        .FirstOrDefaultAsync(o => o.Id == id);

        public async Task<Owners?> GetByUserIdAsync(int userId) =>
            await _context.Owners.Include(o => o.Flat).Include(o => o.Tenants)
                                 .FirstOrDefaultAsync(o => o.UserId == userId);

        public async Task AddAsync(Owners owner)
        {
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Owners owner)
        {
            _context.Owners.Update(owner);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var owner = await GetByIdAsync(id);
            if (owner != null)
            {
                _context.Owners.Remove(owner);
                await _context.SaveChangesAsync();
            }
        }
    }

}
