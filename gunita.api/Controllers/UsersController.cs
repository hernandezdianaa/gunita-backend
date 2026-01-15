using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gunita.Api.Data;
using Gunita.Api.Models;

namespace Gunita.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _context.Users
                .Where(u => !u.IsDeleted)
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserById(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserCreate dto)
        {
            var usernameExists = await _context.Users
                .AnyAsync(u => u.Username == dto.Username && !u.IsDeleted);

            if (usernameExists)
            {
                return Conflict(new
                {
                    message = "Username already exists"
                });
            }

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                BirthDate = dto.BirthDate,
                ContactNumber = dto.ContactNumber,
                Address = dto.Address,
                IsAdmin = dto.IsAdmin,
                IsDeleted = false,
                CreatedDate = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

    }
}
