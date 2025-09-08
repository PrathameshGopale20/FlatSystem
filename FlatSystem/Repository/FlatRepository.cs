using FlatSystem.Data;
using FlatSystem.Dtos;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class FlatRepository : IFlatRepository
    {
        private readonly AppDbContext _context;
        public FlatRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<FlatDto>> GetAllAsync()
        {
            return await _context.Flats
                .Include(f => f.Owner)
                .Include(f => f.Guests)
                .Select(f => new FlatDto
                {
                    Id = f.Id,
                    FlatNo = f.FlatNo,
                    RentAmount = f.RentAmount,
                    Status = f.Status,
                    Owner = f.Owner == null ? null : new OwnerDto
                    {
                        Id = f.Owner.Id,
                        OwnerName = f.Owner.OwnerName,
                        ContactNumber = f.Owner.ContactNumber
                    },
                    Guests = f.Guests.Select(g => new GuestDto
                    {
                        Id = g.Id,
                        GuestName = g.GuestName,
                        ContactNumber = g.ContactNumber
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CreateFlatDto>> GetByApartmentIdAsync(int apartmentId)
        {
            return await _context.Flats
                .Where(f => f.ApartmentId == apartmentId)
                .Select(f => new CreateFlatDto
                {
                    Id = f.Id,
                    ApartmentId = f.ApartmentId,
                    FlatNo = f.FlatNo,
                    RentAmount = f.RentAmount
                })
                .ToListAsync();
        }

        public async Task<FlatDto?> GetByIdAsync(int id)
        {
            return await _context.Flats
                .Include(f => f.Owner)
                .Include(f => f.Guests)
                .Where(f => f.Id == id)
                .Select(f => new FlatDto
                {
                    Id = f.Id,
                    FlatNo = f.FlatNo,
                    RentAmount = f.RentAmount,
                    Status = f.Status,
                    Owner = f.Owner == null ? null : new OwnerDto
                    {
                        Id = f.Owner.Id,
                        OwnerName = f.Owner.OwnerName,
                        ContactNumber = f.Owner.ContactNumber
                    },
                    Guests = f.Guests.Select(g => new GuestDto
                    {
                        Id = g.Id,
                        GuestName = g.GuestName,
                        ContactNumber = g.ContactNumber
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(CreateFlatDto flat)
        {
            var entity = new Flats
            {
                ApartmentId = flat.ApartmentId,
                FlatNo = flat.FlatNo,
                RentAmount = flat.RentAmount,
                Status = "Available"
            };

            _context.Flats.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CreateFlatDto flat)
        {
            var existing = await _context.Flats.FindAsync(flat.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Flat with ID {flat.Id} not found.");

            existing.FlatNo = flat.FlatNo;
            existing.RentAmount = flat.RentAmount;
            // Keep status unchanged unless you plan to allow it here

            _context.Flats.Update(existing);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var flat = await _context.Flats.FindAsync(id);
            if (flat != null)
            {
                _context.Flats.Remove(flat);
                await _context.SaveChangesAsync();
            }
        }
    }
}
