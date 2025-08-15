using FlatSystem.Dtos;
using FlatSystem.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FlatSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogsController : ControllerBase
    {
        private readonly IAuditLogRepository _repo;

        public AuditLogsController(IAuditLogRepository repo)
        {
            _repo = repo;
        }

        // GET: api/AuditLogs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var logs = await _repo.GetAllAsync();
            return Ok(logs);
        }

        // GET: api/AuditLogs/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var logs = await _repo.GetByUserIdAsync(userId);
            return Ok(logs);
        }

        // GET: api/AuditLogs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var log = await _repo.GetByIdAsync(id);
            if (log == null)
                return NotFound();
            return Ok(log);
        }

        // POST: api/AuditLogs
        [HttpPost]
        public async Task<IActionResult> Create(AuditLogDto logDto)
        {
            await _repo.AddAsync(logDto);
            return CreatedAtAction(nameof(GetById), new { id = logDto.Id }, logDto);
        }
    }
}
