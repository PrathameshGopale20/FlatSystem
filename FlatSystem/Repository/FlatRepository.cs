using FlatSystem.Data;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class FlatRepository:IFlatRepository
    {
        private readonly AppDbContext _context;
        public FlatRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Flats>> GetAllAsync() =>
            await _context.Flats.Include(f => f.Owner).ToListAsync();

        public async Task<IEnumerable<Flats>> GetByApartmentIdAsync(int apartmentId) =>
            await _context.Flats.Where(f => f.ApartmentId == apartmentId)
                                .Include(f => f.Owner)
        .ToListAsync();

        public async Task<Flats?> GetByIdAsync(int id) =>
            await _context.Flats.Include(f => f.Owner)
                                .FirstOrDefaultAsync(f => f.Id == id);

        public async Task AddAsync(Flats flat)
        {
            _context.Flats.Add(flat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Flats flat)
        {
            _context.Flats.Update(flat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var flat = await GetByIdAsync(id);
            if (flat != null)
            {
                _context.Flats.Remove(flat);
                await _context.SaveChangesAsync();
            }
        }
    }

}

