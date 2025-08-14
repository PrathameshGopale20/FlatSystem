namespace FlatSystem.Models
{
    public class SecurityGuard
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public Users User { get; set; }

        public int AssignedApartmentId { get; set; }
        public Apartments AssignedApartment { get; set; }

        public string ShiftTiming { get; set; }
        public string ContactNumber { get; set; }

        public DateTime? CreatedAt { get; set; } 
    }
}
