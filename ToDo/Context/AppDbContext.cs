using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Models.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithOne()
                .HasForeignKey(t => t.UserId);

            modelBuilder.Entity<Models.Task>()
                .Property(t => t.Status)
                .HasConversion<string>();


            base.OnModelCreating(modelBuilder);
        }
    }
}
