using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gunita.Api.Data;
using Gunita.Api.Models;

namespace Gunita.Api.Controllers
{
    [ApiController]
    [Route("api/status")]
    public class StatusController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StatusController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetAllStatus()
        {
            var status = await _context.Status
                .ToListAsync();

            return Ok(status);
        }
    }
}
