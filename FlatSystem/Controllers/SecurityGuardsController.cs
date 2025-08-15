using FlatSystem.Dtos;
using FlatSystem.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FlatSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityGuardsController : ControllerBase
    {
        private readonly ISecurityGuardRepository _repo;

        public SecurityGuardsController(ISecurityGuardRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var guard = await _repo.GetByIdAsync(id);
            if (guard == null) return NotFound();
            return Ok(guard);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var guard = await _repo.GetByUserIdAsync(userId);
            if (guard == null) return NotFound();
            return Ok(guard);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSecurityGuardDto guardDto)
        {
            await _repo.AddAsync(guardDto);
            return Ok(new { message = "Security guard created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SecurityGuardDto guardDto)
        {
            if (id != guardDto.Id) return BadRequest("ID mismatch");
            await _repo.UpdateAsync(guardDto);
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
