using FlatSystem.Interface;
using FlatSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlatSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantRepository _repo;

        public TenantsController(ITenantRepository repo) => _repo = repo;

        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetByOwner(int ownerId) =>
            Ok(await _repo.GetByOwnerIdAsync(ownerId));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tenant = await _repo.GetByIdAsync(id);
            if (tenant == null) return NotFound();
            return Ok(tenant);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tenants tenant)
        {
            await _repo.AddAsync(tenant);
            return CreatedAtAction(nameof(Get), new { id = tenant.Id }, tenant);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Tenants tenant)
        {
            if (id != tenant.Id) return BadRequest();
            await _repo.UpdateAsync(tenant);
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
