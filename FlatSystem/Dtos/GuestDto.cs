namespace FlatSystem.Dtos
{
    public class GuestDto
    {
        public int Id { get; set; }
        public string GuestName { get; set; }
        public string ContactNumber { get; set; }
        public string RelationToTenant { get; set; }
        public string Address { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public string? VisitPurpose { get; set; }
        public string? PhotoUrl { get; set; }
    }
}
