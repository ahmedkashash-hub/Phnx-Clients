using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using Phnx.DTOs.Common;
using Phnx.DTOs.Users;

namespace Phnx.DTOs.Activities
{
    public class ActivityAdminResult(Activity entity, List<AdminMiniResult> admins) : BaseDeletableEntityResult<Activity>(entity, admins)
    {
        public ActivityType Type { get; init; } = entity.Type;
        public string Subject { get; init; } = entity.Subject;
        public string? Notes { get; init; } = entity.Notes;
        public DateTime OccurredAt { get; init; } = entity.OccurredAt;
       
        public Guid? ProjectId { get; init; } = entity.ProjectId;
      
    }
}
