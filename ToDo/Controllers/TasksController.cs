using Microsoft.AspNetCore.Mvc; // Keep this import
using Microsoft.AspNetCore.Http.HttpResults;
using ToDo.Context;
using TaskModel = ToDo.Models.Task;
using Microsoft.EntityFrameworkCore;

namespace ToDo.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/Tasks")]
        public ActionResult<IEnumerable<TaskModel>> Get()
        {
            var tasks = _context.Tasks.Select(t => new
            {
                t.Id,
                t.Title,
                t.Description,
                t.Status,
                t.CreatedAt,
                t.UpdatedAt,
                t.UserId,
                User = t.User.Name
            }).ToList();
            if (tasks == null)
            {
                return NotFound("Tasks not found.");
            }
            return Ok(tasks);
        }

        [HttpGet("{id}", Name = "GetNewTask")]
        public ActionResult<TaskModel> Get(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }
            return task;
        }

        [HttpPost]
        public ActionResult Post(TaskModel task)
        {
            if (task == null)
            {
                return BadRequest("Task is null.");
            }
            _context.Tasks.Add(task);
            _context.SaveChanges();

            return new CreatedAtRouteResult("GetNewTask", new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, TaskModel task)
        {
            if (id != task.Id)
            {
                return BadRequest();
            }
            _context.Entry(task).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok("Task updated successfully.");
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound("Task not specified.");
            }
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return Ok("Task deleted successfully.");
        }
    }
}
