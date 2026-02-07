using Phnx.Contracts;
using Phnx.Domain.Enums;
using System.Security.Claims;

namespace Phnx_Clients.Services
{

    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        public Guid Id
            => Guid.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out Guid result) ? result : Guid.Empty;

        private string? GetClaim(string claimName)
        {
            var claims = (httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity)?.Claims;
            return claims?.FirstOrDefault(c => string.Equals(c.Type, claimName, StringComparison.OrdinalIgnoreCase))?.Value;
        }

        public string Name => GetClaim(ClaimTypes.GivenName) ?? string.Empty;
        public string Role => GetClaim(ClaimTypes.Role) ?? string.Empty;

        public Language Language
        {
            get
            {
                var langHeader = httpContextAccessor.HttpContext?
                    .Request
                    .Headers.AcceptLanguage.ToString();

                if (Enum.TryParse(langHeader, ignoreCase: true, out Language language))
                    return language;

                return Language.ar;
            }
        }

        public List<Permission> Permissions
        {
            get
            {
                var permissionClaims = httpContextAccessor.HttpContext?.User?.FindAll("permission");
                if (permissionClaims == null)
                    return [];

                return permissionClaims
                    .Select(c => c.Value)
                    .Where(v => Enum.TryParse<Permission>(v, out _))
                    .Select(v => Enum.Parse<Permission>(v))
                    .ToList();
            }
        }

        public bool HasPermission(Permission permission)
        {
            return Permissions.Contains(permission);
        }
    }
}