using FlatSystem.Interface;
using FlatSystem.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FlatSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _repo;

        public RolesController(IRoleRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _repo.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var role = await _repo.GetByIdAsync(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpGet("name/{roleName}")]
        public async Task<IActionResult> GetByName(string roleName)
        {
            var role = await _repo.GetByNameAsync(roleName);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleDto roleDto)
        {
            await _repo.AddAsync(roleDto);
            return Ok(new { message = "Role created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RoleDto roleDto)
        {
            if (id != roleDto.Id) return BadRequest("ID mismatch");
            await _repo.UpdateAsync(roleDto);
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
