using Newtonsoft.Json;

namespace API.Models.Users
{
    public class UserModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; } = string.Empty;

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}