using FlatSystem.Data;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class RoleRepository:IRoleRepository
    {
        private readonly AppDbContext _context;
        public RoleRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Roles>> GetAllAsync() =>
            await _context.Roles.ToListAsync();

        public async Task<Roles?> GetByIdAsync(int id) =>
            await _context.Roles.FindAsync(id);

        public async Task<Roles?> GetByNameAsync(string roleName) =>
            await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == roleName);

        public async Task AddAsync(Roles role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Roles role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var role = await GetByIdAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
    }

}

