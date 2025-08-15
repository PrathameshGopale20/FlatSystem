using FlatSystem.Dtos;
using FlatSystem.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FlatSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repo) => _repo = repo;

        // GET: api/Users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repo.GetAllAsync();
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null)
                return NotFound(new { message = "User not found" });
            return Ok(user);
        }

        // GET: api/Users/username/john_doe
        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            var user = await _repo.GetByUsernameAsync(username);
            if (user == null)
                return NotFound(new { message = "User not found" });
            return Ok(user);
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repo.AddAsync(createUserDto);

            return Ok(new { message = "User created successfully" });
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserDto userDto)
        {
            if (id != userDto.Id)
                return BadRequest(new { message = "User ID mismatch" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _repo.UpdateAsync(userDto);

            return Ok(new { message = "User updated successfully" });
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return Ok(new { message = "User deleted successfully" });
        }
    }
}
