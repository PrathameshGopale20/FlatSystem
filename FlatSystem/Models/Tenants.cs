using System.ComponentModel.DataAnnotations;

namespace FlatSystem.Models
{
    public class Tenants
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }
        public Owners Owner { get; set; }

        [Required]
        public string TenantName { get; set; } = string.Empty;
        public string PrimaryContactNumber { get; set; } = string.Empty;

        public string FamilyStatus { get; set; } = "Family"; // or "Bachelor"
        public int MembersCount { get; set; }
        public decimal MonthlyRent { get; set; }
        public string PermanentAddress { get; set; }

        public string? ProfileImageUrl { get; set; }
        public string EmergencyContact { get; set; }
        public string? VehicleDetails { get; set; } // could be JSON

        public string AadhaarNumberEncrypted { get; set; } // stored encrypted

        public DateTime?  CreatedAt { get; set; }
    }
}
