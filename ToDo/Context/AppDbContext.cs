using Microsoft.EntityFrameworkCore;

namespace ToDo.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Models.User> Users { get; set; }
    }
}
