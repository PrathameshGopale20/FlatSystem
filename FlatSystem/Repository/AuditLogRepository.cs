using FlatSystem.Data;
using FlatSystem.Dtos;
using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Repository
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly AppDbContext _context;

        public AuditLogRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<AuditLogDto>> GetAllAsync()
        {
            return await _context.AuditLogs
                .Include(a => a.User)
                .Select(a => new AuditLogDto
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    Username = a.User.Username,
                    Action = a.Action,
                    Details = a.Details,
                    Timestamp = a.Timestamp
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<AuditLogDto>> GetByUserIdAsync(int userId)
        {
            return await _context.AuditLogs
                .Where(a => a.UserId == userId)
                .Include(a => a.User)
                .Select(a => new AuditLogDto
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    Username = a.User.Username,
                    Action = a.Action,
                    Details = a.Details,
                    Timestamp = a.Timestamp
                })
                .ToListAsync();
        }

        public async Task<AuditLogDto?> GetByIdAsync(int id)
        {
            return await _context.AuditLogs
                .Include(a => a.User)
                .Where(a => a.Id == id)
                .Select(a => new AuditLogDto
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    Username = a.User.Username,
                    Action = a.Action,
                    Details = a.Details,
                    Timestamp = a.Timestamp
                })
                .FirstOrDefaultAsync();
        }

        public async Task AddAsync(AuditLogDto logDto)
        {
            var entity = new AuditLog
            {
                UserId = logDto.UserId,
                Action = logDto.Action,
                Details = logDto.Details,
                Timestamp = logDto.Timestamp
            };

            _context.AuditLogs.Add(entity);
            await _context.SaveChangesAsync();

            logDto.Id = entity.Id; // Assign generated Id back to DTO
        }
    }
}
