using System.ComponentModel.DataAnnotations;

namespace FlatSystem.Models
{
    public class AuditLog
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public Users User { get; set; }

        [Required]
        public string Action { get; set; } = string.Empty;

        public string Details { get; set; } = string.Empty; // JSON or text

        public DateTime Timestamp { get; set; }
    }
}
