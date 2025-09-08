namespace FlatSystem.Dtos
{
    public class CreateUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string FullName { get; set; }

        public string ContactNumber { get; set; }

        public string? Email { get; set; }

        public int RoleId { get; set; }
    }
}
