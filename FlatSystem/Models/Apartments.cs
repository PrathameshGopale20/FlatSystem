using System.ComponentModel.DataAnnotations;

namespace FlatSystem.Models
{
    public class Apartments
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        public int TotalFlats { get; set; }

        public DateTime? CreatedAt { get; set; }

        // Navigation
        public ICollection<Flats> Flats { get; set; } = new List<Flats>();
    }
}
