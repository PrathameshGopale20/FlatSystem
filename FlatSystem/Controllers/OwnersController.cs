using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var owner = await _repo.GetByIdAsync(id);
            if (owner == null) return NotFound();
            return Ok(owner);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId) =>
            Ok(await _repo.GetByUserIdAsync(userId));

        [HttpPost]
        public async Task<IActionResult> Create(Owners owner)
        {
            await _repo.AddAsync(owner);
            return CreatedAtAction(nameof(Get), new { id = owner.Id }, owner);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Owners owner)
        {
            if (id != owner.Id) return BadRequest();
            await _repo.UpdateAsync(owner);
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
