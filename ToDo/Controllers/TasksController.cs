using AutoMapper;
using Microsoft.AspNetCore.Mvc; // Keep this import
using Microsoft.EntityFrameworkCore;
using ToDo.Context;
using ToDo.DTO;
using ToDo.Repositories;
using TaskModel = ToDo.Models.Task;

namespace ToDo.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repository;
        private readonly IMapper _mapper;

        public TasksController(ITaskRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<TaskDTO>> Get()
        {
            try
            {
                var tasks = await _repository.GetTasks();
                var tasksDto = _mapper.Map<IEnumerable<TaskDTO>>(tasks);
                return Ok(tasksDto);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }


        }

        [HttpGet("{id}", Name = "GetNewTask")]
        public async Task<ActionResult<TaskDTO>> Get(int id)
        {
            try
            {
                var task = await _repository.GetTask(id);
                if (task == null)
                {
                    return NotFound("Task not found.");
                }

                var taskDto = _mapper.Map<TaskDTO>(task);
                return Ok(taskDto);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TaskDTO>> Post(TaskDTO taskDto)
        {
            try
            {
                if (taskDto == null)
                {
                    return BadRequest("Task is null.");
                }

                var task = _mapper.Map<TaskModel>(taskDto);

                var newTask = await _repository.CreateTask(task);

                var newTaskDto = _mapper.Map<TaskDTO>(newTask);

                return new CreatedAtRouteResult("GetNewTask", new { id = newTask.Id }, newTask);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDTO>> Put(int id, [FromBody] TaskDTO taskDto)
        {
            try
            {
                var existingTask = await _repository.GetTask(id);
                if (existingTask == null)
                {
                    return NotFound("Task not found.");
                }


                var task = _mapper.Map(taskDto, existingTask);
                var updatedTask = await _repository.UpdateTask(id, existingTask);
                var updatedTaskDto = _mapper.Map<TaskDTO>(updatedTask);
                
                return Ok(updatedTaskDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TaskDTO>> Delete(int id)
        {
            try
            {
                var task = await _repository.GetTask(id);
                if (task == null)
                {
                    return NotFound("Task not specified.");
                }

                var taskDeleted = await _repository.DeleteTask(id);
                var taskDeletedDto = _mapper.Map<TaskDTO>(taskDeleted);
                return Ok(taskDeletedDto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpGet("SumCompletedTasks")]
        public async Task<IActionResult> CountTasksCompletedByUser(int id)
        {
            try
            {
                var userName = await _repository.GetUserNameById(id);
                var countTasksCompleted = await _repository.CountTasksCompletedByUser(id);
                var message = $"O usuário {userName} completou: {countTasksCompleted} tarefas";
                return Ok(message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpGet("SumCompletedTasksByUserInTheCurrentMonth")]
        public async Task<IActionResult> CountTasksCompletedByUserCurrentMonth(int id)
        {
            try
            {
                var userName = await _repository.GetUserNameById(id);
                var countTasksCompletedInTheCurrentMonth = await _repository.CountTasksCompletedByUserCurrentMonth(id);
                var message = $"O usuário {userName} completou: {countTasksCompletedInTheCurrentMonth} tarefas neste mês";
                return Ok(message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }
    }
}
