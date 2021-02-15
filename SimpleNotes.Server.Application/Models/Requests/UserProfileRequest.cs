using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SimpleNotes.Server.Application.Models.Requests
{
    public class UserProfileRequest
    {
        [DefaultValue("https://www.example.com/images/avatar.png")]
        public string AvatarUrl { get; set; }

        [JsonPropertyName("name")]
        [DefaultValue("Adriano Giudice")]
        public string Username { get; set; }
    }
}
