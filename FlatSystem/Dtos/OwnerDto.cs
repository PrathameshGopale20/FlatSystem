namespace FlatSystem.Dtos
{
    public class OwnerDto
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string? ProfileImageUrl { get; set; }
        public List<TenantDto> Tenants { get; set; }
    }
}
