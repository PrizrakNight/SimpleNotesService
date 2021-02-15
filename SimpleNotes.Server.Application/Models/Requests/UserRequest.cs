using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SimpleNotes.Server.Application.Models.Requests
{
    public class UserRequest
    {
        [JsonPropertyName("name")]
        [DefaultValue("Adriano Giudice")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [DefaultValue("My_SecretPassw0rd")]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
