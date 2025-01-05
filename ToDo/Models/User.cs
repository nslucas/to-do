﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class User
    {
        public User()
        {
            Tasks = new Collection<Task>();
        }
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

        public ICollection<Task>? Tasks { get; set; }
    }
}
