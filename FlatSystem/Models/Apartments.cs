using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

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

        public int SocietyId { get; set; }
        public Society Society { get; set; }


        // Navigation
        public ICollection<Flats> Flats { get; set; } = new List<Flats>();
    }
}
