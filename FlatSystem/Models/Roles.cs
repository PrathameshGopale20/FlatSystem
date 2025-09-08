using System.ComponentModel.DataAnnotations;

namespace FlatSystem.Models
{
    public class Roles
    {
        public int Id { get; set; }

        [Required]
        public string RoleName { get; set; } = string.Empty;
        // e.g. "Secretary", "Owner", "SecurityGuard"

        public ICollection<Users> Users { get; set; } = new List<Users>();
    }
}
