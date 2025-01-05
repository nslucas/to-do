using System.Collections.ObjectModel;

namespace ToDo.Models
{
    public class User
    {
        public User()
        {
            Tasks = new Collection<Task>();
        }
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        public ICollection<Task>? Tasks { get; set; }
    }
}
