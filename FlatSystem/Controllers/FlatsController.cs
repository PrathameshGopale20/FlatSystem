using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlatSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatsController : ControllerBase
    {
        private readonly IFlatRepository _repo;

        public FlatsController(IFlatRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var flat = await _repo.GetByIdAsync(id);
            if (flat == null) return NotFound();
            return Ok(flat);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Flats flat)
        {
            await _repo.AddAsync(flat);
            return CreatedAtAction(nameof(Get), new { id = flat.Id }, flat);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Flats flat)
        {
            if (id != flat.Id) return BadRequest();
            await _repo.UpdateAsync(flat);
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
