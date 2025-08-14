namespace FlatSystem.Dtos
{
    public class CreateOwnerDto
    {
        public int UserId { get; set; }
        public int FlatId { get; set; }
        public string OwnerName { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
