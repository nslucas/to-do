using System.ComponentModel.DataAnnotations;

namespace ToDo.DTO
{
    public class UserDTO
    {
        [Required]
        [StringLength(80)]
        public required string Name { get; set; }
        [Required]
        [StringLength(300)]
        public required string Email { get; set; }
    }
}
