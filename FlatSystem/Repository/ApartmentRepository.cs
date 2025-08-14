using FlatSystem.Data;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class ApartmentRepository:IApartmentRepository
    {
        private readonly AppDbContext _context;
        public ApartmentRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Apartments>> GetAllAsync() =>
            await _context.Apartments.ToListAsync();

        public async Task<Apartments?> GetByIdAsync(int id) =>
            await _context.Apartments.FirstOrDefaultAsync(a => a.Id == id);

        public async Task AddAsync(Apartments apartment)
        {
            _context.Apartments.Add(apartment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Apartments apartment)
        {
            _context.Apartments.Update(apartment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var apt = await GetByIdAsync(id);
            if (apt != null)
            {
                _context.Apartments.Remove(apt);
                await _context.SaveChangesAsync();
            }
        }
    }
}

