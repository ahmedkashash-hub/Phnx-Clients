
using Phnx.Domain.Entities;
using Phnx.DTOs.Common;
using Phnx.DTOs.Users;
using Phnx.Shared.Constants;

namespace Phnx.DTOs.Advertisements;


public class AdvertisementAdminResult(Advertisement entity,List<AdminMiniResult> admins) : BaseAuditResult<Advertisement>(entity, admins)
{
    public string ImageUrl { get; init; } = AppConstants.GetImagePath(entity.ImageUrl);
    public string? Title { get; init; } = entity.Title;
    public string? ActionUrl { get; init; }= entity.ActionUrl;

}
