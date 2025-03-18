using System.ComponentModel.DataAnnotations;

namespace ToDo.DTO
{
    public class UserCreateDTO
    {
        [Required]
        [StringLength(80)]
        public required string Name { get; set; }
        [Required]
        [StringLength(300)]
        public required string Email { get; set; }
        [Required]
        [StringLength(300)]
        public required string Password { get; set; }
    }
}
