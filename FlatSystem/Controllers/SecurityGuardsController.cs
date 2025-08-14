using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var guard = await _repo.GetByIdAsync(id);
            if (guard == null) return NotFound();
            return Ok(guard);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId) =>
            Ok(await _repo.GetByUserIdAsync(userId));

        [HttpPost]
        public async Task<IActionResult> Create(SecurityGuard guard)
        {
            await _repo.AddAsync(guard);
            return CreatedAtAction(nameof(Get), new { id = guard.Id }, guard);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SecurityGuard guard)
        {
            if (id != guard.Id) return BadRequest();
            await _repo.UpdateAsync(guard);
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
