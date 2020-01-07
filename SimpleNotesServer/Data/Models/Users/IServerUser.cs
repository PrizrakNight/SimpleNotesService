namespace SimpleNotesServer.Data.Models.Users
{
    public interface IServerUser
    {
        string Name { get; set; }

        string PasswordHash { get; set; }

        string Role { get; set; }

        long RegistrationDate { get; set; }

        long LastEntrance { get; set; }
    }
}