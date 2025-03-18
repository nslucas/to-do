using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ToDo.Models;

namespace ToDo.DTO
{
    public class TaskDTO
    {
        [Required]
        [StringLength(80)]
        public required string Title { get; set; }
        [StringLength(300)]
        public string? Description { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusTask Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
    }
}
