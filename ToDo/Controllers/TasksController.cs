using Microsoft.AspNetCore.Mvc; // Keep this import
using Microsoft.EntityFrameworkCore;
using ToDo.Context;
using ToDo.Repositories;
using TaskModel = ToDo.Models.Task;

namespace ToDo.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repository;

        public TasksController(ITaskRepository repository)
        {
            _repository = repository;
        }

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tasks = await _repository.GetTasks();
                return Ok(tasks);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }


        }

        [HttpGet("{id}", Name = "GetNewTask")]
        public async Task<ActionResult<TaskModel>> Get(int id)
        {
            try
            {
                var task = await _repository.GetTask(id);
                if (task == null)
                {
                    return NotFound("Task not found.");
                }
                return Ok(task);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post(TaskModel task)
        {
            try
            {
                if (task == null)
                {
                    return BadRequest("Task is null.");
                }


                var taskCreated = await _repository.CreateTask(task);

                return new CreatedAtRouteResult("GetNewTask", new { id = taskCreated.Id }, taskCreated);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TaskModel task)
        {
            try
            {
                // Log para depuração
                Console.WriteLine($"ID recebido: {task.Id}");
                Console.WriteLine($"ID da rota: {id}");

                if (id != task.Id)
                {
                    return BadRequest();
                }
               var updatedTask = await _repository.UpdateTask(id, task);
                return Ok(updatedTask);
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
                var task = await _repository.GetTask(id);
                if (task == null)
                {
                    return NotFound("Task not specified.");
                }

                var taskDeleted = await _repository.DeleteTask(id);
                return Ok(taskDeleted);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "There was a problem while processing your request. Please contact our support");
            }
        }

        [HttpGet("SomarTasksCompletas")]
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
    }
}
