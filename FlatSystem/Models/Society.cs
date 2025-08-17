using System.ComponentModel.DataAnnotations;

namespace FlatSystem.Models
{
    public class Society
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string SocietyName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property (if you want to link apartments)
        public ICollection<Apartments> Apartments { get; set; } = new List<Apartments>();
    }
}
