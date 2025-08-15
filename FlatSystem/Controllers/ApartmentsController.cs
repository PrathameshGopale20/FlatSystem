using FlatSystem.Dtos;
using FlatSystem.Interface;
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
            var apartments = await _repo.GetAllAsync();
            return Ok(apartments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var apt = await _repo.GetByIdAsync(id);
            if (apt == null) return NotFound();
            return Ok(apt);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateApartmentDto apartment)
        {
            await _repo.AddAsync(apartment);
            return Ok(new { message = "Apartment created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ApartmentDto apartment)
        {
            if (id != apartment.Id) return BadRequest("ID mismatch");

            await _repo.UpdateAsync(apartment);
            return Ok(new { message = "Apartment updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return Ok(new { message = "Apartment deleted successfully" });
        }
    }
}
