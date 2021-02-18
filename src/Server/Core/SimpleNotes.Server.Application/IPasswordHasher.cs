namespace SimpleNotes.Server.Application
{
    public interface IPasswordHasher
    {
        string HashPassword(string rawPassword);

        bool ComparePassword(string password, string passwordHash);
    }
}
