using FlatSystem.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                return Ok(await _repo.GetAllAsync());
            }

            [HttpGet("user/{userId}")]
            public async Task<IActionResult> GetByUser(int userId)
            {
                return Ok(await _repo.GetByUserIdAsync(userId));
            }
        }
}

