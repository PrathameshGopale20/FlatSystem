namespace FlatSystem.Dtos
{
    public class CreateFlatDto
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public string FlatNo { get; set; }
        public decimal RentAmount { get; set; }
    }
}
