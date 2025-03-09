namespace ToDo.Repositories;
using TaskModel = ToDo.Models.Task;
public interface ITaskRepository
{
    Task<IEnumerable<TaskModel>> GetTasks();
    Task<TaskModel> GetTask(int id);
    Task<TaskModel> CreateTask(Models.Task task);
    Task<TaskModel> UpdateTask(int id, Models.Task task);
    Task<TaskModel> DeleteTask(int id);
    Task<int> CountTasksCompletedByUser(int id);
    Task<string> GetUserNameById(int id);
}
