using System.ComponentModel;

namespace SimpleNotes.Server.Application.Models.Requests
{
    public class UserProfileRequest
    {
        [DefaultValue("https://www.example.com/images/avatar.png")]
        public string AvatarUrl { get; set; }

        [DefaultValue("Adriano Giudice")]
        public string Username { get; set; }
    }
}
