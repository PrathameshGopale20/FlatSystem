using System.ComponentModel.DataAnnotations;

namespace FlatSystem.Models
{
    public class Guest
    {
        public int Id { get; set; }

        [Required]
        public string GuestName { get; set; } = string.Empty;
        public string ContactNumber { get; set; }
        public string AadhaarNumberEncrypted { get; set; } // encrypted

        public int VisitingFlatId { get; set; }
        public Flats VisitingFlat { get; set; }

        public string RelationToTenant { get; set; }
        public string Address { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public DateTime VisitDate { get; set; }
        public string? VisitPurpose { get; set; }
        public string? PhotoUrl { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
