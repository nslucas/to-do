using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Models;
using ToDo.Repositories;

namespace ToDo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UsersController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            try
            {
                var users = await _repository.GetUsers();

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
                var user = await _repository.GetUser(id);

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
        public async Task<ActionResult> Post(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User is null.");
                }
                
                var userCreated = await _repository.CreateUser(user);
                return new CreatedAtRouteResult("GetNewUser", new { id = user.Id }, user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, User user)
        {
            try
            {
                if (id != user.Id)
                {
                    return BadRequest("User ID mismatch.");
                }
                var userUpdated = await _repository.UpdateUser(id, user);
                return Ok(userUpdated);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var userExist = await _repository.GetUser(id);
                if (userExist == null)
                {
                    return NotFound("User not found.");
                }

                var user = await _repository.DeleteUser(id);
                return Ok(user);
            }
            
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }
    }
}
