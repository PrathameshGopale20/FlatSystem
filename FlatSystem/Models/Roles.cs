using System.ComponentModel.DataAnnotations;

namespace FlatSystem.Models
{
    public class Roles
    {
        public int Id { get; set; }

        [Required]
        public string RoleName { get; set; } = string.Empty;
        // e.g. "Secretary", "Owner", "SecurityGuard"

        public string? Description { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public ICollection<Users> Users { get; set; } = new List<Users>();
    }
}
