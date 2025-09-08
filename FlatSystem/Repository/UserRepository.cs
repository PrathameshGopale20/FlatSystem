using FlatSystem.Data;
using FlatSystem.Dtos;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FlatSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    FullName = u.FullName,
                    ContactNumber = u.ContactNumber,
                    Email = u.Email,
                    Role = new RoleDto
                    {
                        Id = u.Role.Id,
                        RoleName = u.Role.RoleName,
                    }
                })
                .ToListAsync();
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Id == id)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    FullName = u.FullName,
                    ContactNumber = u.ContactNumber,
                    Email = u.Email,
                    Role = new RoleDto
                    {
                        Id = u.Role.Id,
                        RoleName = u.Role.RoleName
                    }
                })
                .FirstOrDefaultAsync();
        }

        public async Task<UserDto?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Where(u => u.Username == username)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    FullName = u.FullName,
                    ContactNumber = u.ContactNumber,
                    Email = u.Email,
                    Role = new RoleDto
                    {
                        Id = u.Role.Id,
                        RoleName = u.Role.RoleName
                    }
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(CreateUserDto createUserDto)
        {
            // Generate salt and hash
            using var hmac = new HMACSHA256();
            var passwordSalt = Convert.ToBase64String(hmac.Key);
            var passwordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(createUserDto.Password)));

            var user = new Users
            {
                Username = createUserDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FullName = createUserDto.FullName,
                ContactNumber = createUserDto.ContactNumber,
                Email = createUserDto.Email,
                RoleId = createUserDto.RoleId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserDto userDto)
        {
            var user = await _context.Users.FindAsync(userDto.Id);
            if (user != null)
            {
                user.Username = userDto.Username;
                user.FullName = userDto.FullName;
                user.ContactNumber = userDto.ContactNumber;
                user.Email = userDto.Email;
                user.RoleId = userDto.Role.Id;
                user.UpdatedAt = DateTime.UtcNow;

                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
