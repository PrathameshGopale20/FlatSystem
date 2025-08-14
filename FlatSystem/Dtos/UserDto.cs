namespace FlatSystem.Dtos
{
    public class UserDto
    {
            public int Id { get; set; }
            public string Username { get; set; }
            public string FullName { get; set; }
            public string ContactNumber { get; set; }
            public string? Email { get; set; }
            public RoleDto Role { get; set; }
    }
}
