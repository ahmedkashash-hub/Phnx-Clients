

using Phnx.Domain.Enums;
using Phnx.Shared.Constants;

namespace Phnx.DTOs.Events;

public class EventResult(string name, string description, EventType eventType, DateTime date, List<string> imageUrls)
{
    public string Name => name;
    public string Description => description;
    public EventType EventType => eventType;
    public DateTime Date => date;
    public List<string> ImageUrls { get; init; } = AppConstants.GetImagesPaths(imageUrls);
}
