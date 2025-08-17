using FlatSystem.Data;
using FlatSystem.Dtos;
using FlatSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlatSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocietyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SocietyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var societies = await _context.Societies
                .Select(s => new SocietyDto
                {
                    Id = s.Id,
                    SocietyName = s.SocietyName,
                    Address = s.Address
                })
                .ToListAsync();

            return Ok(societies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var society = await _context.Societies
                .Where(s => s.Id == id)
                .Select(s => new SocietyDto
                {
                    Id = s.Id,
                    SocietyName = s.SocietyName,
                    Address = s.Address
                })
                .FirstOrDefaultAsync();

            if (society == null)
                return NotFound();

            return Ok(society);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSocietyDto dto)
        {
            var society = new Society
            {
                SocietyName = dto.SocietyName,
                Address = dto.Address,
                CreatedAt = DateTime.UtcNow
            };

            _context.Societies.Add(society);
            await _context.SaveChangesAsync();

            var societyDto = new SocietyDto
            {
                Id = society.Id,
                SocietyName = society.SocietyName,
                Address = society.Address
            };

            return CreatedAtAction(nameof(Get), new { id = society.Id }, societyDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateSocietyDto dto)
        {
            var society = await _context.Societies.FindAsync(id);
            if (society == null)
                return NotFound();

            society.SocietyName = dto.SocietyName;
            society.Address = dto.Address;

            _context.Societies.Update(society);
            await _context.SaveChangesAsync();

            return Ok(new SocietyDto
            {
                Id = society.Id,
                SocietyName = society.SocietyName,
                Address = society.Address
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var society = await _context.Societies.FindAsync(id);
            if (society == null)
                return NotFound();

            _context.Societies.Remove(society);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
