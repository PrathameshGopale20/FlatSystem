using FlatSystem.Interface;
using FlatSystem.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FlatSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerRepository _repo;

        public OwnersController(IOwnerRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var owner = await _repo.GetByIdAsync(id);
            if (owner == null) return NotFound();
            return Ok(owner);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var owner = await _repo.GetByUserIdAsync(userId);
            if (owner == null) return NotFound();
            return Ok(owner);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOwnerDto ownerDto)
        {
            await _repo.AddAsync(ownerDto);
            return Ok(new { message = "Owner created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, OwnerDto ownerDto)
        {
            if (id != ownerDto.Id) return BadRequest("ID mismatch");
            await _repo.UpdateAsync(ownerDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
