namespace SimpleNotesServer.Sdk.Models.User
{
    public class DetailedUserModel : UserModel
    {
        public string AccessToken { get; set; }

        public string Role { get; set; }

        public long RegistrationDate { get; set; }

        public long LastEntrance { get; set; }
    }
}
