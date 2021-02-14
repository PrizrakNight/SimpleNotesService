using System.ComponentModel;

namespace SimpleNotes.Server.Application.Models.Responses
{
    public class UserProfileResponse
    {
        [DefaultValue("Adriano Giudice")]
        public string Name { get; set; }

        [DefaultValue("https://www.example.com/images/avatar.png")]
        public string AvatarUrl { get; set; }

        [DefaultValue("User")]
        public string Role { get; set; }

        [DefaultValue("Some_Access_Token")]
        public string AccessToken { get; set; }

        [DefaultValue(592133123)]
        public long RegistrationDate { get; set; }

        [DefaultValue(592133123)]
        public long LastEntrance { get; set; }
    }
}
