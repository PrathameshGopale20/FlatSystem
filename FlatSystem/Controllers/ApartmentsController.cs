using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlatSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsController : ControllerBase
    {
        private readonly IApartmentRepository _repo;

        public ApartmentsController(IApartmentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var apt = await _repo.GetByIdAsync(id);
            if (apt == null) return NotFound();
            return Ok(apt);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Apartments apartment)
        {
            await _repo.AddAsync(apartment);
            return CreatedAtAction(nameof(Get), new { id = apartment.Id }, apartment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Apartments apartment)
        {
            if (id != apartment.Id) return BadRequest();
            await _repo.UpdateAsync(apartment);
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
    