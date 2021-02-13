using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SimpleNotes.Server.Application.Options;
using SimpleNotes.Server.Domain.Entities;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SimpleNotes.Server.Application.Services.Implementation
{
    internal class TokenService : ITokenService
    {
        private readonly TokenProviderOptions _options;

        public TokenService(TokenProviderOptions options)
        {
            _options = options;
        }

        public Task<string> GenerateTokenAsync(SimpleUser simpleUser)
        {
            var claimsIdentity = CreateIdentity(simpleUser);
            var jwtSecurityToken = GetJwtSecurityToken(claimsIdentity);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            return Task.FromResult(jwtSecurityTokenHandler.WriteToken(jwtSecurityToken));
        }

        private JwtSecurityToken GetJwtSecurityToken(ClaimsIdentity claimsIdentity)
        {
            var utcNow = DateTime.UtcNow;
            var expiration = utcNow.Add(TimeSpan.FromHours(_options.TokenExpiration));
            var signingCredentials = GetSigningCredentials();

            return new JwtSecurityToken(_options.Issuer, _options.Audience, claimsIdentity.Claims, utcNow, expiration, signingCredentials);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var appSecretBytes = Encoding.ASCII.GetBytes(_options.ApplicationSecret);
            var securityKey = new SymmetricSecurityKey(appSecretBytes);

            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }

        private ClaimsIdentity CreateIdentity(SimpleUser simpleUser)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.GivenName, simpleUser.Name),
                new Claim(ClaimTypes.Role, simpleUser.Role),
                new Claim(ClaimValueTypes.Integer32, simpleUser.Key.ToString())
            };

            return new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
        }
    }
}
