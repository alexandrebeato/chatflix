using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API.Models.Users
{
    public class CreateUserModel
    {
        public CreateUserModel()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; }
        public DateTime CreatedAt { get; }

        [Required]
        [JsonProperty("userName")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; } = string.Empty;
    }
}