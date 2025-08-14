namespace FlatSystem.Dtos
{
    public class FlatDto
    {
        public int Id { get; set; }
        public string FlatNo { get; set; }
        public decimal RentAmount { get; set; }
        public string Status { get; set; }
        public OwnerDto Owner { get; set; }
        public List<GuestDto> Guests { get; set; }
    }
}
