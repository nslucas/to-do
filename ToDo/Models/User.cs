using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDo.Models
{
    public class User
    {
        public User()
        {
            Tasks = new Collection<Task>();
        }
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public required string Name { get; set; }
        [Required]
        [StringLength(300)]
        public required string Email { get; set; }
        [Required]
        [StringLength(300)]
        public required string Password { get; set; }
        [JsonIgnore]
        public ICollection<Task>? Tasks { get; set; }
    }
}
