using Newtonsoft.Json;

namespace API.Models.Messages
{
    public class UserModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; } = string.Empty;
    }
}