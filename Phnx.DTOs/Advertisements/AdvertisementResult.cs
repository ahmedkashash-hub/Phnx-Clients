using Phnx.Shared.Constants;

namespace Phnx.DTOs.Advertisements;

public record AdvertisementResult(string? Title,string ImageUrl,string? ActionUrl)
{
    public string ImageUrl { get; init; } = AppConstants.GetImagePath(ImageUrl);
}
