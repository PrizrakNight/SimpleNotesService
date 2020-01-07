using System.Text.RegularExpressions;
using SimpleNotesClient.Models.Authentication;

namespace SimpleNotesClient.Managers
{
    public static class AuthManager
    {
        public static AutharizationSuccess AutharizationSuccess { get; set; }

        private static readonly Regex _passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");

        private static readonly Regex _usernameRegex = new Regex(@"^[a-zA-Z0-9_-]{3,20}$");

        public static bool UsernameIsValid(string username) =>
            !string.IsNullOrEmpty(username) && _usernameRegex.IsMatch(username);

        public static bool PasswordIsValid(string password) =>
            !string.IsNullOrEmpty(password) && _passwordRegex.IsMatch(password);
    }
}