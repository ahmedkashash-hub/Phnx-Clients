
using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using Phnx.DTOs.Common;
using Phnx.DTOs.Users;
using Phnx.Shared.Constants;

namespace Aiport.DTOs.Events;

public class EventAdminResult(Event entity, List<AdminMiniResult> admins) : BaseDeletableEntityResult<Event>(entity, admins)
{
    public string Name { get; init; } = entity.Name;
    public string Description { get; init; } = entity.Description;
    public EventType EventType { get; init; } = entity.EventType;
    public DateTime Date { get; init; } = entity.Date;
    public List<string> ImageUrls { get; init; } = AppConstants.GetImagesPaths(entity.ImageUrls);
}
