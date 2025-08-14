using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface IAuditLogRepository
    {
        Task<IEnumerable<AuditLog>> GetAllAsync();
        Task<IEnumerable<AuditLog>> GetByUserIdAsync(int userId);
        Task<AuditLog?> GetByIdAsync(int id);
        Task AddAsync(AuditLog log);
    }
}
