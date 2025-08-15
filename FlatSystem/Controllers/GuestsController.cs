using FlatSystem.Dtos;
using FlatSystem.Interface;
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
        public async Task<IActionResult> Create(CreateGuestDto guestDto)
        {
            await _repo.AddAsync(guestDto);
            // Since ID is generated in the DB, you might need to re-fetch if you want to return it
            return Ok(new { message = "Guest created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GuestDto guestDto)
        {
            if (id != guestDto.Id) return BadRequest("ID mismatch");
            await _repo.UpdateAsync(guestDto);
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
