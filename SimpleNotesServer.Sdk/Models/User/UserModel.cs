using System.Text.Json.Serialization;

namespace SimpleNotesServer.Sdk.Models.User
{
    public class UserModel
    {
        [JsonPropertyName("name")]
        public string Username { get; set; }

        [JsonPropertyName("avatarUrl")]
        public string AvatarUrl { get; set; }
    }
}
