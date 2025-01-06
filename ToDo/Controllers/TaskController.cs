using Microsoft.AspNetCore.Mvc; // Keep this import
using Microsoft.AspNetCore.Http.HttpResults;
using ToDo.Context;
using TaskModel = ToDo.Models.Task;

namespace ToDo.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/tasks")]
        public ActionResult<IEnumerable<TaskModel>> Get()
        {
            var tasks = _context.Tasks.ToList();
            if (tasks == null)
            {
                return NotFound("Tasks not found.");
            }
            return tasks;
        }

        [HttpGet("{id}", Name = "GetNewTask")]
        public ActionResult<TaskModel> Get(int id)
        {
            var task = _context.Tasks.FirstOrDefault(u => u.Id == id);
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
            _context.Tasks.Update(task);
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
