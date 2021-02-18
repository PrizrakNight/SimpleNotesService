using System;
using System.Linq;
using System.Security.Cryptography;

namespace SimpleNotes.Server.Application
{
    internal class PasswordHasher : IPasswordHasher
    {
        public bool ComparePassword(string password, string passwordHash)
        {
            byte[] buffer4;
            var buffer3 = new byte[0x20];
            var dst = new byte[0x10];

            if (string.IsNullOrEmpty(passwordHash) || string.IsNullOrEmpty(password)) return false;

            var src = Convert.FromBase64String(passwordHash);

            if ((src.Length != 0x31) || (src[0] != 0))
                return false;

            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }

            return buffer3.SequenceEqual(buffer4);
        }

        public string HashPassword(string rawPassword)
        {
            byte[] salt;
            byte[] buffer2;
            var dst = new byte[0x31];

            if (string.IsNullOrEmpty(rawPassword)) throw new ArgumentNullException("rawPassword");

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(rawPassword, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }

            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);

            return Convert.ToBase64String(dst);
        }
    }
}
