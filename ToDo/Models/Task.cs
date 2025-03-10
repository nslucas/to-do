﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDo.Models
{
    public class Task
    {
        [JsonIgnore]
        public int Id { get; set; }
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
        [JsonIgnore]
        public User? User { get; set; }
    }
}
