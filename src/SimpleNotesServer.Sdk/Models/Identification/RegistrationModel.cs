namespace SimpleNotesServer.Sdk.Models.Identification
{
    public class RegistrationModel : AuthorizationModel
    {
        public string ConfirmPassword { get; set; }

        public string AvatarUrl { get; set; }
    }
}
