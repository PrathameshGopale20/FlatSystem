using FlatSystem.Dtos;
using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface IAuditLogRepository
    {
        Task<IEnumerable<AuditLogDto>> GetAllAsync();
        Task<IEnumerable<AuditLogDto>> GetByUserIdAsync(int userId);
        Task<AuditLogDto?> GetByIdAsync(int id);
        Task AddAsync(AuditLogDto log);
    }
}
