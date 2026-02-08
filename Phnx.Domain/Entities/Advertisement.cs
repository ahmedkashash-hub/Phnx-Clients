
using Phnx.Domain.Common;

namespace Phnx.Domain.Entities;

public class Advertisement : BaseAuditableEntity
{
    private Advertisement() { }
    private Advertisement(string? title, string imageUrl,string? actionUrl)
    {
        Title = title;
        ImageUrl = imageUrl;
        ActionUrl = actionUrl;
    }
    public static Advertisement Create(string? title, string imageUrl,string? actionUrl) => new(title, imageUrl,actionUrl);
    public string ImageUrl { get; private set; } = string.Empty;
    public string? Title { get; private set; } = string.Empty;
    public string? ActionUrl { get;private set;  }
    public void Update(string? title, string imageUrl,string? actionUrl)
    {
        Title = title;
        ImageUrl = imageUrl;
        ActionUrl = actionUrl;  
    }
}
