using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Context;
using ToDo.Models;

namespace ToDo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            try
            {
                var users = await _context.Users.Select(u => new
                {
                    u.Id,
                    u.Name,
                    u.Email,
                    Tasks = u.Tasks.Select(t => new
                    { t.Id, t.Title, t.Description })
                }).AsNoTracking()
                .ToListAsync();
                if (users == null)
                {
                    return NotFound("Users not found.");
                }
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpGet("{id}", Name = "GetNewUser")]
        public async Task<ActionResult<User>> Get(int id)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user == null)
                {
                    return NotFound("User not found.");
                }
                return user;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpPost]
        public ActionResult Post(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User is null.");
                }
                _context.Users.Add(user);
                _context.SaveChanges();
                return new CreatedAtRouteResult("GetNewUser", new { id = user.Id }, user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, User user)
        {
            try
            {
                if (id != user.Id)
                {
                    return BadRequest();
                }
                _context.Users.Update(user);
                _context.SaveChanges();
                return Ok("User updated successfully.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == id);
                if (user == null)
                {
                    return NotFound("User not specified.");
                }
                _context.Users.Remove(user);
                _context.SaveChanges();
                return Ok("User deleted successfully.");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }
    }
}
