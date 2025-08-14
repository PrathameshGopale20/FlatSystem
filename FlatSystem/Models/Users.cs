using System.ComponentModel.DataAnnotations;
using System.Data;

namespace FlatSystem.Models
{
    public class Users
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public string PasswordSalt { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string? Email { get; set; }

        public int RoleId { get; set; } // FK
        public Roles Role { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        // Navigation
        public Owners? Owner { get; set; } // only if this user is an Owner
        public SecurityGuard? SecurityGuard { get; set; } // only if guard
    }
}
