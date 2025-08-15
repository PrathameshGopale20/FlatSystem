using FlatSystem.Data;
using FlatSystem.Dtos;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly AppDbContext _context;
        public OwnerRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<OwnerDto>> GetAllAsync()
        {
            return await _context.Owners
                .Include(o => o.Tenants)
                .Select(o => new OwnerDto
                {
                    Id = o.Id,
                    OwnerName = o.OwnerName,
                    ContactNumber = o.ContactNumber,
                    Address = o.Address,
                    ProfileImageUrl = o.ProfileImageUrl,
                    Tenants = o.Tenants.Select(t => new TenantDto
                    {
                        Id = t.Id,
                        TenantName = t.TenantName,
                        PrimaryContactNumber = t.PrimaryContactNumber,
                        PermanentAddress = t.PermanentAddress,
                        ProfileImageUrl = t.ProfileImageUrl
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<OwnerDto?> GetByIdAsync(int id)
        {
            return await _context.Owners
                .Include(o => o.Tenants)
                .Where(o => o.Id == id)
                .Select(o => new OwnerDto
                {
                    Id = o.Id,
                    OwnerName = o.OwnerName,
                    ContactNumber = o.ContactNumber,
                    Address = o.Address,
                    ProfileImageUrl = o.ProfileImageUrl,
                    Tenants = o.Tenants.Select(t => new TenantDto
                    {
                        Id = t.Id,
                        TenantName = t.TenantName,
                        PrimaryContactNumber = t.PrimaryContactNumber,
                        PermanentAddress = t.PermanentAddress,
                        ProfileImageUrl = t.ProfileImageUrl
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CreateOwnerDto?> GetByUserIdAsync(int userId)
        {
            return await _context.Owners
                .Where(o => o.UserId == userId)
                .Select(o => new CreateOwnerDto
                {
                    UserId = o.UserId,
                    FlatId = o.FlatId,
                    OwnerName = o.OwnerName,
                    ContactNumber = o.ContactNumber,
                    Address = o.Address,
                    ProfileImageUrl = o.ProfileImageUrl
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(CreateOwnerDto owner)
        {
            var entity = new Owners
            {
                UserId = owner.UserId,
                FlatId = owner.FlatId,
                OwnerName = owner.OwnerName,
                ContactNumber = owner.ContactNumber,
                Address = owner.Address,
                ProfileImageUrl = owner.ProfileImageUrl
            };

            _context.Owners.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OwnerDto owner)
        {
            var existing = await _context.Owners.FindAsync(owner.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Owner with ID {owner.Id} not found.");

            existing.OwnerName = owner.OwnerName;
            existing.ContactNumber = owner.ContactNumber;
            existing.Address = owner.Address;
            existing.ProfileImageUrl = owner.ProfileImageUrl;

            _context.Owners.Update(existing);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner != null)
            {
                _context.Owners.Remove(owner);
                await _context.SaveChangesAsync();
            }
        }
    }
}
