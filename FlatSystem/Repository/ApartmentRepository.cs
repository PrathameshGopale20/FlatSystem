using FlatSystem.Data;
using FlatSystem.Dtos;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly AppDbContext _context;

        public ApartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApartmentDto>> GetAllAsync()
        {
            return await _context.Apartments
                .Select(a => new ApartmentDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Address = a.Address,
                    TotalFlats = a.TotalFlats
                })
                .ToListAsync();
        }

        public async Task<ApartmentDto?> GetByIdAsync(int id)
        {
            return await _context.Apartments
                .Where(a => a.Id == id)
                .Select(a => new ApartmentDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Address = a.Address,
                    TotalFlats = a.TotalFlats
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(CreateApartmentDto apartment)
        {
            var entity = new Apartments
            {
                Name = apartment.Name,
                Address = apartment.Address,
                TotalFlats = apartment.TotalFlats
            };

            _context.Apartments.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ApartmentDto apartment)
        {
            var existing = await _context.Apartments.FindAsync(apartment.Id);
            if (existing == null)
                throw new KeyNotFoundException($"Apartment with ID {apartment.Id} not found.");

            existing.Name = apartment.Name;
            existing.Address = apartment.Address;
            existing.TotalFlats = apartment.TotalFlats;

            _context.Apartments.Update(existing);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.Apartments.FindAsync(id);
            if (existing != null)
            {
                _context.Apartments.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}
