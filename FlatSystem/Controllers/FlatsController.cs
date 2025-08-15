using FlatSystem.Dtos;
using FlatSystem.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FlatSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatsController : ControllerBase
    {
        private readonly IFlatRepository _repo;

        public FlatsController(IFlatRepository repo) => _repo = repo;

        // GET: api/Flats
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var flats = await _repo.GetAllAsync();
            return Ok(flats);
        }

        // GET: api/Flats/apartment/5
        [HttpGet("apartment/{apartmentId}")]
        public async Task<IActionResult> GetByApartmentId(int apartmentId)
        {
            var flats = await _repo.GetByApartmentIdAsync(apartmentId);
            return Ok(flats);
        }

        // GET: api/Flats/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var flat = await _repo.GetByIdAsync(id);
            if (flat == null)
                return NotFound();
            return Ok(flat);
        }

        // POST: api/Flats
        [HttpPost]
        public async Task<IActionResult> Create(CreateFlatDto flatDto)
        {
            await _repo.AddAsync(flatDto);
            return CreatedAtAction(nameof(Get), new { id = flatDto.ApartmentId }, flatDto);
        }

        // PUT: api/Flats/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, FlatDto flatDto)
        {
            if (id != flatDto.Id)
                return BadRequest();

            await _repo.UpdateAsync(flatDto);
            return NoContent();
        }

        // DELETE: api/Flats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return NoContent();
        }
    }
}
