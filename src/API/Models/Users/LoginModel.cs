using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API.Models.Users
{
    public class LoginModel
    {
        [Required]
        [JsonProperty("username")]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; } = string.Empty;
    }
}