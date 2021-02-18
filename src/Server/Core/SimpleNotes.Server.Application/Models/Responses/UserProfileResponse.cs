using System.ComponentModel;
using System.Text.Json.Serialization;

namespace SimpleNotes.Server.Application.Models.Responses
{
    public class UserProfileResponse
    {
        [JsonPropertyName("username")]
        [DefaultValue("Adriano Giudice")]
        public string Name { get; set; }

        [JsonPropertyName("avatarUrl")]
        [DefaultValue("https://www.example.com/images/avatar.png")]
        public string AvatarUrl { get; set; }

        [JsonPropertyName("role")]
        [DefaultValue("User")]
        public string Role { get; set; }

        [JsonPropertyName("accessToken")]
        [DefaultValue("Some_Access_Token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("registrationDate")]
        [DefaultValue(592133123)]
        public long RegistrationDate { get; set; }

        [JsonPropertyName("lastEntrance")]
        [DefaultValue(592133123)]
        public long LastEntrance { get; set; }
    }
}
