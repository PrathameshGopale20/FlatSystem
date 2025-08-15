using FlatSystem.Data;
using FlatSystem.Dtos;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class SecurityGuardRepository : ISecurityGuardRepository
    {
        private readonly AppDbContext _context;
        public SecurityGuardRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<SecurityGuardDto>> GetAllAsync()
        {
            return await _context.SecurityGuards
                .Include(sg => sg.AssignedApartment)
                .Select(sg => new SecurityGuardDto
                {
                    Id = sg.Id,
                    AssignedApartmentId = sg.AssignedApartmentId,
                    ShiftTiming = sg.ShiftTiming,
                    ContactNumber = sg.ContactNumber
                })
                .ToListAsync();
        }

        public async Task<SecurityGuardDto?> GetByIdAsync(int id)
        {
            return await _context.SecurityGuards
                .Include(sg => sg.AssignedApartment)
                .Where(sg => sg.Id == id)
                .Select(sg => new SecurityGuardDto
                {
                    Id = sg.Id,
                    AssignedApartmentId = sg.AssignedApartmentId,
                    ShiftTiming = sg.ShiftTiming,
                    ContactNumber = sg.ContactNumber
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CreateSecurityGuardDto?> GetByUserIdAsync(int userId)
        {
            return await _context.SecurityGuards
                .Include(sg => sg.AssignedApartment)
                .Where(sg => sg.UserId == userId)
                .Select(sg => new CreateSecurityGuardDto
                {
                    UserId = sg.UserId,
                    AssignedApartmentId = sg.AssignedApartmentId,
                    ShiftTiming = sg.ShiftTiming,
                    ContactNumber = sg.ContactNumber
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(CreateSecurityGuardDto guard)
        {
            var entity = new SecurityGuard
            {
                UserId = guard.UserId,
                AssignedApartmentId = guard.AssignedApartmentId,
                ShiftTiming = guard.ShiftTiming,
                ContactNumber = guard.ContactNumber
            };

            _context.SecurityGuards.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SecurityGuardDto guard)
        {
            var existing = await _context.SecurityGuards.FindAsync(guard.Id);
            if (existing == null)
                throw new KeyNotFoundException($"SecurityGuard with ID {guard.Id} not found.");

            existing.AssignedApartmentId = guard.AssignedApartmentId;
            existing.ShiftTiming = guard.ShiftTiming;
            existing.ContactNumber = guard.ContactNumber;

            _context.SecurityGuards.Update(existing);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var guard = await _context.SecurityGuards.FindAsync(id);
            if (guard != null)
            {
                _context.SecurityGuards.Remove(guard);
                await _context.SaveChangesAsync();
            }
        }
    }
}
