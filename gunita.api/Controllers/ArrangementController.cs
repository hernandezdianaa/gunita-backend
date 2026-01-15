using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gunita.Api.Data;
using Gunita.Api.Models;

namespace Gunita.Api.Controllers
{
    [ApiController]
    [Route("api/arrangements")]
    public class ArrangementsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ArrangementsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Arrangement>>> GetAllArrangements()
        {
            var arrangements = await _context.Arrangements
                .ToListAsync();

            return Ok(arrangements);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Arrangement>>> GetArrangementById(int id)
        {
            var arrangement = await _context.Arrangements
                .FirstOrDefaultAsync(a => a.Id == id);

            if (arrangement == null)
            {
                return NotFound();
            }

            return Ok(arrangement);
        }

        [HttpPost]
        public async Task<ActionResult<Arrangement>> CreateArrangement(CreateArrangements dto)
        {
            var arrangement = new Arrangement
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                CreatedDate = DateTime.UtcNow
            };

             _context.Arrangements.Add(arrangement);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetArrangementById), new { id = arrangement.Id }, arrangement);
        }
    }
}
