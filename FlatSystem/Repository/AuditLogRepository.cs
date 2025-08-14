using FlatSystem.Data;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AppDbContext _context;
        public AuditLogRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<AuditLog>> GetAllAsync() =>
            await _context.AuditLogs.Include(a => a.User).ToListAsync();

        public async Task<IEnumerable<AuditLog>> GetByUserIdAsync(int userId) =>
            await _context.AuditLogs.Where(a => a.UserId == userId).Include(a => a.User).ToListAsync();

        public async Task<AuditLog?> GetByIdAsync(int id) =>
            await _context.AuditLogs.Include(a => a.User)
                                    .FirstOrDefaultAsync(a => a.Id == id);

        public async Task AddAsync(AuditLog log)
        {
            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
