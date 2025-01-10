using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class Task
    {
        public int Id { get; set; }
        [Required]
        [StringLength(80)]
        public required string Title { get; set; }
        [StringLength(300)]
        public string? Description { get; set; }
        [Required]
        public char Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
