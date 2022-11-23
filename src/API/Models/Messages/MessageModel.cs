using Newtonsoft.Json;

namespace API.Models.Messages
{
    public class MessageModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("roomId")]
        public Guid RoomId { get; set; }

        [JsonProperty("user")]
        public UserModel? User { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; } = string.Empty;
    }
}