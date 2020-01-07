using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace SimpleNotesServer.Options
{
    public static class AuthorizationOptions
    {
        /// <summary>Издатель токена</summary>
        public static string Issuer => "SimpleNotesServer";

        /// <summary>Потребитель токена</summary>
        public static string Audience => "https://localhost:44330";

        /// <summary>Секретный ключ токена</summary>
        public static string Sercret_Key => "Super Secret Key JWT Brans UE93";

        /// <summary>Продолжительность существования токена в минутах</summary>
        public static double TokenLifeTime => 15;

        /// <summary>Симметричный ключ безопасности</summary>
        public static SymmetricSecurityKey SecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Sercret_Key));
    }
}