using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.DTO;
using ToDo.Models;
using ToDo.Repositories;

namespace ToDo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<UserDTO>>> Get()
        {
            try
            {
                var users = await _repository.GetUsers();

                var usersDto = _mapper.Map<IEnumerable<UserDTO>>(users);

                return Ok(usersDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpGet("{id}", Name = "GetNewUser")]
        public async Task<ActionResult<UserDTO>> Get(int id)
        {
            try
            {
                var user = await _repository.GetUser(id);

                if (user == null)
                {
                    return NotFound("User not found.");

                }

                var userDto = _mapper.Map<UserDTO>(user); 
                return userDto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserCreateDTO>> Post(UserCreateDTO userCreateDTO)
        {
            try
            {
                if (userCreateDTO == null)
                {
                    return BadRequest("User is null.");
                }

                var user = _mapper.Map<User>(userCreateDTO);
                var userCreated = await _repository.CreateUser(user);
                var userCreatedDto = _mapper.Map<UserCreateDTO>(userCreated);
                return new CreatedAtRouteResult("GetNewUser", userCreatedDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> Put(int id, UserDTO userDto)
        {
            try
            {
                var user = await _repository.GetUser(id);

                if (user == null)
                {
                    return BadRequest("User not found.");
                }
                // Aplique as alterações do userDto ao user
                var userUpdate = _mapper.Map(userDto, user);
                var userUpdated = await _repository.UpdateUser(id, userUpdate);
                var updatedUserDto = _mapper.Map<UserDTO>(userUpdated);
                return Ok(updatedUserDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDTO>> Delete(int id)
        {
            try
            {
                var userExist = await _repository.GetUser(id);
                if (userExist == null)
                {
                    return NotFound("User not found.");
                }

                var user = await _repository.DeleteUser(id);
                var userDeletedDto = _mapper.Map<UserDTO>(user);
                return Ok(userDeletedDto);
            }
            
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }
    }
}
