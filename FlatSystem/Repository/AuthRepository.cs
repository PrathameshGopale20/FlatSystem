using FlatSystem.Data;
using FlatSystem.Dtos;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FlatSystem.Service
{
    public class AuthRepository : IAuth
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Users> _hasher;

        public AuthRepository(AppDbContext context)
        {
            _context = context;
            _hasher = new PasswordHasher<Users>();
        }

        public async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<Users> Register(RegisterDto dto)
        {
            // Check if any Super Admin user exists (RoleId = 1)
            bool superAdminExists = await _context.Users.AnyAsync(u => u.RoleId == 1);
            if (superAdminExists)
                throw new InvalidOperationException("Super Admin Already Exists");

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == dto.RolesId);
            if (role == null)
                throw new Exception("Role Does Not Exist");

            // Ensure first registration must be Super Admin
            if (dto.RolesId != 1)
                throw new InvalidOperationException("First registration must be Super Admin");

            var user = new Users
            {
                FullName = dto.Name,
                Email = dto.Email,
                RoleId = role.Id
            };
            user.PasswordHash = _hasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == user.Email);
        }

        public async Task<Users?> Login(LoginDto dto)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == dto.Email.ToLower());

            if (user == null)
                return null;

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Success)
                return user;

            return null;
        }
    }
}
