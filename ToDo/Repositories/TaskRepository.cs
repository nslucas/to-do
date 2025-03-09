using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Context;
using ToDo.Models;
using TaskModel = ToDo.Models.Task;


namespace ToDo.Repositories
{
    public class TaskRepository  : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskModel> GetTask(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TaskModel>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }


        public async Task<TaskModel> CreateTask(TaskModel task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }
        public async Task<TaskModel> UpdateTask(int id, TaskModel task)
        {
            var existingTask = await _context.Tasks.FindAsync(id); 
            if (existingTask == null)
            {
                throw new Exception("Task not found.");
            }

            existingTask.Title = task.Title;
            existingTask.Description = task.Description;
            existingTask.Status = task.Status;

            _context.Entry(existingTask).State = EntityState.Modified; 
            await _context.SaveChangesAsync(); 

            return existingTask;
        }

        public async Task<TaskModel> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            return task;

        }

        public async Task<int> CountTasksCompletedByUser(int id)
        {
            return await _context.Set<TaskModel>().Where(t => t.UserId == id && t.Status == 'C').CountAsync();
        }

        public async Task<string> GetUserNameById(int id)
        {
            var user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Id == id);
            return user?.Name ?? "Usuário não encontrado";
        }
    }

}
