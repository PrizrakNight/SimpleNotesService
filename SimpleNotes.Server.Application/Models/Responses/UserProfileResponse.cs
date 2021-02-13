namespace SimpleNotes.Server.Application.Models.Responses
{
    public class UserProfileResponse
    {
        public string Name { get; set; }

        public string AvatarUrl { get; set; }

        public string Role { get; set; }

        public string AccessToken { get; set; }

        public long RegistrationDate { get; set; }

        public long LastEntrance { get; set; }

        public NoteResponse[] Notes { get; set; }
    }
}
