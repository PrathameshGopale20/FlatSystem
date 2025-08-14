namespace FlatSystem.Dtos
{
    public class CreateSecurityGuardDto
    {
        public int UserId { get; set; }
        public int AssignedApartmentId { get; set; }
        public string ShiftTiming { get; set; }
        public string ContactNumber { get; set; }
    }
}
