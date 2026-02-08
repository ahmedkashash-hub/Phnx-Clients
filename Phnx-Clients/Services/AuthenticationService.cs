using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Phnx.Contracts;
using Phnx.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;



namespace Phnx_Clients.Services
{


    public class AuthenticationService(IConfiguration configuration) : IAUthenticationService
    {
        private static string GenerateRefreshToken()
        {
            byte[]? randomNumber = new byte[32];
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public (Guid? id, string role) GetIdFromTokenPrinciple(string token)
        {
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? throw new KeyNotFoundException("JWT key was not found!"))),
                ValidIssuer = configuration["Jwt:Issuer"] ?? throw new KeyNotFoundException("JWT Issuer was not found!"),
                ValidAudience = configuration["Jwt:Audience"] ?? throw new KeyNotFoundException("JWT Audience was not found!")
            };
            JwtSecurityTokenHandler tokenHandler = new();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                return (null, "");

            string? returendId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(returendId, out Guid id))
            {
                string role = principal?.FindFirstValue(ClaimTypes.Role) ?? "";
                if (!string.IsNullOrEmpty(role))
                    return (id, role);
            }
            return (null, "");
        }


        public Task<(string token, string refreshToken)> Login(Guid id, string name, Language language, List<Claim>? claims)
        {
            string refreshToken = GenerateRefreshToken();
            JwtSecurityTokenHandler tokenHandler = new();
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(
                [
                    new(ClaimTypes.NameIdentifier, id.ToString()),
                new(ClaimTypes.GivenName,name),
                new("accept-language",language.ToString()),
            ]),
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddSeconds(int.Parse(configuration["Jwt:AccessExpireSeconds"] ?? throw new KeyNotFoundException("JWT Expire was not found"))),
                Issuer = configuration["Jwt:Issuer"] ?? throw new KeyNotFoundException("JWT Issuer was not found!"),
                Audience = configuration["Jwt:Audience"] ?? throw new KeyNotFoundException("JWT Audience was not found!"),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:key"] ?? throw new KeyNotFoundException("JWT Key was not found"))),
                        SecurityAlgorithms.HmacSha256Signature),
            };
            if (claims != null)
                tokenDescriptor.Subject.AddClaims(claims);
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);
            return Task.FromResult((token, refreshToken));
        }
    }
}