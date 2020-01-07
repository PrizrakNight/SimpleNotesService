using System;
using System.Security.Claims;
using System.Security.Cryptography;
using SimpleNotesServer.Data.Models.Users;

namespace SimpleNotesServer.Extensions.Entity
{
    public static class UsersExtensions
    {
        public static ClaimsIdentity CreateIdentity(this IServerUser serverUser) => new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, serverUser.Name),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, serverUser.Role)

        }, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

        public static bool ComparePassword(this IServerUser serverUser, string password)
        {
            byte[] hashBytes = Convert.FromBase64String(serverUser.PasswordHash);

            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            var deriveBytes = new Rfc2898DeriveBytes(password, salt, 10000);

            byte[] hash = deriveBytes.GetBytes(20);

            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;

            return true;
        }

        public static string ToHash(this string password)
        {
            byte[] salt;

            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var deriveBytes = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = deriveBytes.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }
    }
}