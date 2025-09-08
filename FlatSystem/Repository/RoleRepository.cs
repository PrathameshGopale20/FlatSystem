using FlatSystem.Data;
using FlatSystem.Dtos;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;
        public RoleRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            return await _context.Roles
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    RoleName = r.RoleName,
                })
                .ToListAsync();
        }

        public async Task<RoleDto?> GetByIdAsync(int id)
        {
            return await _context.Roles
                .Where(r => r.Id == id)
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    RoleName = r.RoleName,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<RoleDto?> GetByNameAsync(string roleName)
        {
            return await _context.Roles
                .Where(r => r.RoleName == roleName)
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    RoleName = r.RoleName,
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(RoleDto role)
        {
            var entity = new Roles
            {
                RoleName = role.RoleName,
            };

            _context.Roles.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RoleDto role)
        {
            var existing = await _context.Roles.FindAsync(role.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Role with ID {role.Id} not found.");

            existing.RoleName = role.RoleName;
            _context.Roles.Update(existing);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }
    }
}
