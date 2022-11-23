using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API.Models.Messages
{
    public class CreateMessageModel
    {
        public CreateMessageModel()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        [Required]
        [JsonProperty("roomId")]
        public Guid RoomId { get; set; }

        [Required]
        [JsonProperty("content")]
        public string Content { get; set; } = string.Empty;
    }
}