using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlatSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly IGuestRepository _repo;

        public GuestsController(IGuestRepository repo) => _repo = repo;

        [HttpGet("flat/{flatId}")]
        public async Task<IActionResult> GetByFlat(int flatId) =>
            Ok(await _repo.GetByFlatIdAsync(flatId));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var guest = await _repo.GetByIdAsync(id);
            if (guest == null) return NotFound();
            return Ok(guest);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guest guest)
        {
            await _repo.AddAsync(guest);
            return CreatedAtAction(nameof(Get), new { id = guest.Id }, guest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Guest guest)
        {
            if (id != guest.Id) return BadRequest();
            await _repo.UpdateAsync(guest);
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
