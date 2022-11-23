using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API.Models.Rooms
{
    public class CreateRoomModel
    {
        public CreateRoomModel()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
    }
}