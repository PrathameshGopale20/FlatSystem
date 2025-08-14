using FlatSystem.Data;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class TenantRepository : ITenantRepository
    {
        private readonly AppDbContext _context;
        public TenantRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Tenants>> GetByOwnerIdAsync(int ownerId) =>
            await _context.Tenants.Where(t => t.OwnerId == ownerId).ToListAsync();

        public async Task<Tenants?> GetByIdAsync(int id) =>
            await _context.Tenants.FirstOrDefaultAsync(t => t.Id == id);

        public async Task AddAsync(Tenants tenant)
        {
            _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tenants tenant)
        {
            _context.Tenants.Update(tenant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tenant = await GetByIdAsync(id);
            if (tenant != null)
            {
                _context.Tenants.Remove(tenant);
                await _context.SaveChangesAsync();
            }
        }
    }
}
