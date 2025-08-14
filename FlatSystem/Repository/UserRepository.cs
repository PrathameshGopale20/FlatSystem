using FlatSystem.Data;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Users>> GetAllAsync() =>
            await _context.Users.Include(u => u.Role).ToListAsync();

        public async Task<Users?> GetByIdAsync(int id) =>
            await _context.Users.Include(u => u.Role)
                                .FirstOrDefaultAsync(u => u.Id == id);

        public async Task<Users?> GetByUsernameAsync(string username) =>
            await _context.Users.Include(u => u.Role)
                                .FirstOrDefaultAsync(u => u.Username == username);

        public async Task AddAsync(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Users user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }

}

