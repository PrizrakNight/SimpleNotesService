using System.Text.Json.Serialization;

namespace SimpleNotesServer.Sdk.Models.Identification
{
    public class AuthorizationModel
    {
        [JsonPropertyName("name")]
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
