using FlatSystem.Data;
using FlatSystem.Dtos;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class GuestRepository : IGuestRepository
    {
        private readonly AppDbContext _context;
        public GuestRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<GuestDto>> GetByFlatIdAsync(int flatId)
        {
            return await _context.Guests
                .Where(g => g.VisitingFlatId == flatId)
                .Select(g => new GuestDto
                {
                    Id = g.Id,
                    GuestName = g.GuestName,
                    ContactNumber = g.ContactNumber,
                    RelationToTenant = g.RelationToTenant,
                    Address = g.Address,
                    EntryTime = g.EntryTime,
                    ExitTime = g.ExitTime,
                    VisitPurpose = g.VisitPurpose,
                    PhotoUrl = g.PhotoUrl
                })
                .ToListAsync();
        }

        public async Task<GuestDto?> GetByIdAsync(int id)
        {
            return await _context.Guests
                .Where(g => g.Id == id)
                .Select(g => new GuestDto
                {
                    Id = g.Id,
                    GuestName = g.GuestName,
                    ContactNumber = g.ContactNumber,
                    RelationToTenant = g.RelationToTenant,
                    Address = g.Address,
                    EntryTime = g.EntryTime,
                    ExitTime = g.ExitTime,
                    VisitPurpose = g.VisitPurpose,
                    PhotoUrl = g.PhotoUrl
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(CreateGuestDto guest)
        {
            var entity = new Guest
            {
                GuestName = guest.GuestName,
                ContactNumber = guest.ContactNumber,
                AadhaarNumberEncrypted = guest.AadhaarNumber,
                VisitingFlatId = guest.VisitingFlatId,
                RelationToTenant = guest.RelationToTenant,
                Address = guest.Address,
                EntryTime = guest.EntryTime,
                ExitTime = guest.ExitTime,
                VisitPurpose = guest.VisitPurpose,
                PhotoUrl = guest.PhotoUrl
            };

            _context.Guests.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(GuestDto guest)
        {
            var existing = await _context.Guests.FindAsync(guest.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Guest with ID {guest.Id} not found.");

            existing.GuestName = guest.GuestName;
            existing.ContactNumber = guest.ContactNumber;
            existing.RelationToTenant = guest.RelationToTenant;
            existing.Address = guest.Address;
            existing.EntryTime = guest.EntryTime;
            existing.ExitTime = guest.ExitTime;
            existing.VisitPurpose = guest.VisitPurpose;
            existing.PhotoUrl = guest.PhotoUrl;

            _context.Guests.Update(existing);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var guest = await _context.Guests.FindAsync(id);
            if (guest != null)
            {
                _context.Guests.Remove(guest);
                await _context.SaveChangesAsync();
            }
        }
    }
}
