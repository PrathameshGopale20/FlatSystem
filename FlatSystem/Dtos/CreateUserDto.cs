namespace FlatSystem.Dtos
{
    public class CreateUserDto
    {
        public string Username { get; set; }

        // Raw password provided by user during creation; 
        // will be hashed and salted in the service/controller before saving
        public string Password { get; set; }

        public string FullName { get; set; }

        public string ContactNumber { get; set; }

        public string? Email { get; set; }

        // Links to an existing Role ID in Roles table (Secretary / Owner / SecurityGuard)
        public int RoleId { get; set; }
    }
}
