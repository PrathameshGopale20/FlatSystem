using FlatSystem.Data;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class SecurityGuardRepository : ISecurityGuardRepository
    {
        private readonly AppDbContext _context;
        public SecurityGuardRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<SecurityGuard>> GetAllAsync() =>
            await _context.SecurityGuards.Include(sg => sg.AssignedApartment).ToListAsync();

        public async Task<SecurityGuard?> GetByIdAsync(int id) =>
            await _context.SecurityGuards.Include(sg => sg.AssignedApartment)
                                         .FirstOrDefaultAsync(sg => sg.Id == id);

        public async Task<SecurityGuard?> GetByUserIdAsync(int userId) =>
            await _context.SecurityGuards.Include(sg => sg.AssignedApartment)
                                         .FirstOrDefaultAsync(sg => sg.UserId == userId);

        public async Task AddAsync(SecurityGuard guard)
        {
            _context.SecurityGuards.Add(guard);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SecurityGuard guard)
        {
            _context.SecurityGuards.Update(guard);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var guard = await GetByIdAsync(id);
            if (guard != null)
            {
                _context.SecurityGuards.Remove(guard);
                await _context.SaveChangesAsync();
            }
        }
    }
}
