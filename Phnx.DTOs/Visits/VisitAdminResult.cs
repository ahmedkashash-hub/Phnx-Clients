using Phnx.Domain.Entities;
using Phnx.DTOs.Common;
using Phnx.DTOs.Users;

namespace Phnx.DTOs.Visits;

public class VisitAdminResult(Visit entity, List<AdminMiniResult> admins) : BaseDeletableEntityResult<Visit>(entity, admins)
{
    public Guid ClientId { get; init; } = entity.ClientId;
    public DateTime VisitTime { get; init; } = entity.VisitTime;
    public string Note { get; init; } = entity.Note;
}
