namespace ToDo.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public char Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
    }
}
