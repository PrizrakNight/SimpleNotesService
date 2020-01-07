using Microsoft.IdentityModel.Tokens;
using SimpleNotesServer.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SimpleNotesServer.Extensions.IdentityPrincipial
{
    public static class IdentityPrincipialExtensions
    {
        public static JwtSecurityToken CreateToken(this ClaimsIdentity identity)
        {
            DateTime utcNow = DateTime.UtcNow;

            return new JwtSecurityToken(
                issuer: AuthorizationOptions.Issuer,
                audience: AuthorizationOptions.Audience,
                notBefore: utcNow,
                claims: identity.Claims,
                expires: utcNow.Add(TimeSpan.FromMinutes(AuthorizationOptions.TokenLifeTime)),
                signingCredentials: new SigningCredentials(AuthorizationOptions.SecurityKey(),
                    SecurityAlgorithms.HmacSha256));
        }

        public static string TokenToString(this JwtSecurityToken token) =>
            new JwtSecurityTokenHandler().WriteToken(token);
    }
}