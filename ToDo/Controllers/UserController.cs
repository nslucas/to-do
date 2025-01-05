using Microsoft.AspNetCore.Mvc;
using ToDo.Context;
using ToDo.Models;

namespace ToDo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            var users = _context.Users.ToList();
            if (users == null)
            {
                return NotFound("Users not found.");
            }
            return users;
        }

        [HttpGet("{id}", Name="GetNewUser")]
        public ActionResult<User> Get(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return user;
        }

        [HttpPost]
        public ActionResult Post(User user)
        {
            if(user == null)
            {
                return BadRequest("User is null.");
            }
            _context.Users.Add(user);
            _context.SaveChanges();
           
            return new CreatedAtRouteResult("GetNewUser", new {id = user.Id}, user);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            _context.Users.Update(user);
            _context.SaveChanges();
            return Ok("User updated successfully.");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
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
    }
}
