using Microsoft.AspNetCore.Mvc;

namespace Phnx_Clients
{

    public record RequestHeaders(
        [FromHeader(Name = "accept-language")] string? AcceptLanguage,
        [FromHeader(Name = "X-App-Version")] string? AppVersion,
        [FromHeader(Name = "X-App-Platform")] string? AppPlatform
    );
}
