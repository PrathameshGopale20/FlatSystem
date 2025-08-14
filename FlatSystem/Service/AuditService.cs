using FlatSystem.Data;
using FlatSystem.Models;

namespace FlatSystem.Service
{
    public class AuditService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditService(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Records an audit log entry for the currently authenticated user.
        /// </summary>
        /// <param name="action">Description of the action performed</param>
        /// <param name="details">Optional details (JSON or text)</param>
        public async Task LogAsync(string action, string details = "")
        {
            // Extract UserId from JWT claims
            var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            var log = new AuditLog
            {
                UserId = userId,
                Action = action,
                Details = details,
                Timestamp = DateTime.UtcNow
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves all audit logs.
        /// </summary>
        public async Task<List<AuditLog>> GetAllAsync()
        {
            return await Task.FromResult(_context.AuditLogs.ToList());
        }

        /// <summary>
        /// Retrieves audit logs for a specific user.
        /// </summary>
        public async Task<List<AuditLog>> GetByUserIdAsync(int userId)
        {
            return await Task.FromResult(_context.AuditLogs
                .Where(log => log.UserId == userId)
                .ToList());
        }
    }
}
