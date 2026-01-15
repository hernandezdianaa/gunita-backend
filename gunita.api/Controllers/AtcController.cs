using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gunita.Api.Data;
using Gunita.Api.Models;

namespace Gunita.Api.Controllers
{
    [ApiController]
    [Route("api/atc")]
    public class AtcController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AtcController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Atc>>> GetAllOrders()
        {
            var orders = await _context.Atc
                .ToListAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Atc>>> GetAtcById(int id)
        {
            var atc = await _context.Atc
                .FirstOrDefaultAsync(a => a.Id == id);

            if (atc == null)
            {
                return NotFound();
            }

            return Ok(atc);
        }

        [HttpPost]
        public async Task<ActionResult<Atc>> CreateAtc(CreateAtc dto)
        {
            var atc = new Atc
            {
                UserId = dto.UserId,
                ArrangementId = dto.ArrangementId,
                Quantity = dto.Quantity,
                Status = dto.Status,
                CreatedDate = DateTime.UtcNow
            };

            _context.Atc.Add(atc);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAtcById), new { id = atc.Id }, atc);
        }
    }
}
