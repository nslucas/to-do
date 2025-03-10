using Microsoft.EntityFrameworkCore;
using ToDo.Context;
using ToDo.Models;

namespace ToDo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.ToArrayAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUser(int id, User user)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                throw new Exception("User not found.");
            }
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return existingUser;
        }

        public async Task<User> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
