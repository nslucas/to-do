using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BCrypt.Net;

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
        [JsonIgnore]
        public string Password { get; private set; }
        [JsonIgnore]
        public ICollection<Task>? Tasks { get; set; }

        public void SetPassword(string password)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Método para verificar se a senha fornecida corresponde ao hash armazenado
        public bool VerifyPassword(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Password); // Verifica a senha
        }
    }
}
