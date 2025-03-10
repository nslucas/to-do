using ToDo.Models;

namespace ToDo.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(int id);      
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(int id, User user);
        Task<User> DeleteUser(int id);

    }
}
