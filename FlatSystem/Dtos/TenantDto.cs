namespace FlatSystem.Dtos
{
    public class TenantDto
    {
        public int Id { get; set; }
        public string TenantName { get; set; }
        public string PrimaryContactNumber { get; set; }
        public string FamilyStatus { get; set; }
        public int MembersCount { get; set; }
        public decimal MonthlyRent { get; set; }
        public string PermanentAddress { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string EmergencyContact { get; set; }
        public string? VehicleDetails { get; set; }
    }
}
