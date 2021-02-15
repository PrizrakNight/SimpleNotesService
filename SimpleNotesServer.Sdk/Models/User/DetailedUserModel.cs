using System.Text.Json.Serialization;

namespace SimpleNotesServer.Sdk.Models.User
{
    public class DetailedUserModel : UserModel
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("registrationDate")]
        public long RegistrationDate { get; set; }

        [JsonPropertyName("lastEntrance")]
        public long LastEntrance { get; set; }
    }
}
