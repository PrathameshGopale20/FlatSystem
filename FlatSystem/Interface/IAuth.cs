using FlatSystem.Dtos;
using FlatSystem.Models;

namespace FlatSystem.Interface
{
    public interface IAuth
    {
        Task<bool> UserExists(string email);
        Task<Users> Login(LoginDto dto);
        Task<Users> Register(RegisterDto dto);
    }
}
