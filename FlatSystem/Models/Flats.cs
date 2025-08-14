using System.ComponentModel.DataAnnotations;

namespace FlatSystem.Models
{
    public class Flats
    {
        public int Id { get; set; }

        public int ApartmentId { get; set; }
        public Apartments Apartment { get; set; }

        [Required]
        public string FlatNo { get; set; } = string.Empty;

        public decimal RentAmount { get; set; }
        public string Status { get; set; } = "Vacant"; // Occupied / Vacant

        public DateTime? CreatedAt { get; set; }

        // Navigation
        public Owners Owner { get; set; }
        public ICollection<Guest> Guests { get; set; } = new List<Guest>();

    }
}
