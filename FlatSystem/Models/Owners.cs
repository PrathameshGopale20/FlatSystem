using System.ComponentModel.DataAnnotations;

namespace FlatSystem.Models
{
    public class Owners
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public Users User { get; set; }

        public int FlatId { get; set; }
        public Flats Flat { get; set; }

        [Required]
        public string OwnerName { get; set; } = string.Empty;
        public string ContactNumber { get; set; }
        public string Address { get; set; }

        public string? ProfileImageUrl { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<Tenants> Tenants { get; set; } = new List<Tenants>();
    }
}
