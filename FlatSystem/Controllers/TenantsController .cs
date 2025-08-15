using FlatSystem.Dtos;
using FlatSystem.Interface;
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
        public async Task<IActionResult> Create([FromBody] CreateTenantDto tenantDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _repo.AddAsync(tenantDto);
            return Ok(new { message = "Tenant created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TenantDto tenantDto)
        {
            if (id != tenantDto.Id) return BadRequest("Tenant ID mismatch.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _repo.UpdateAsync(tenantDto);
            return Ok(new { message = "Tenant updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return Ok(new { message = "Tenant deleted successfully" });
        }
    }
}
