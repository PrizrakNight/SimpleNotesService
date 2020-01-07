using SimpleNotesServer.Data.Models.Users;
using SimpleNotesServer.Extensions.Entity;
using SimpleNotesServer.Extensions.IdentityPrincipial;

namespace SimpleNotesServer.Data.Models.Responses
{
    public struct AuthSuccess
    {
        public readonly string Message;

        public readonly string Access_Token;

        public readonly string For_User;

        public AuthSuccess(string message, IServerUser serverUser)
        {
            Message = message;
            For_User = serverUser.Name;
            Access_Token = serverUser.CreateIdentity().CreateToken().TokenToString();
        }
    }
}