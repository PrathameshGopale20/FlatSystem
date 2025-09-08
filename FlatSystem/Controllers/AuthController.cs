using FlatSystem.Dtos;
using FlatSystem.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlatSystem.Service;
using ProjectManegementTool.Services;

namespace FlatSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authRepo;
        private readonly TokenService _tokenService;

        public AuthController(IAuth authRepo, TokenService tokenService)
        {
            _authRepo = authRepo;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _authRepo.UserExists(dto.Email))
            {
                return BadRequest("Email already exists");
            }
            try
            {
                var user = await _authRepo.Register(dto);
                var token = _tokenService.CreateToken(user);
                return Ok(new AuthResponceDto { Success = true, Token = token });
            }

            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = "Super Admin Already Exists." });
            }
            catch (Exception ex)
            {
                string details = ex.InnerException?.Message ?? ex.Message;
                return BadRequest(new { message = details });
            }

        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _authRepo.Login(dto);
            if (user == null)
                return Unauthorized("Invalid username or password");

            var token = _tokenService.CreateToken(user);
            return Ok(new AuthResponceDto { Success = true, Token = token });
        }
    }
}
