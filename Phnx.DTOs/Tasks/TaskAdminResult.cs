using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using TaskStatus = Phnx.Domain.Enums.TaskStatus;
using Phnx.DTOs.Common;
using Phnx.DTOs.Users;

namespace Phnx.DTOs.Tasks
{
    public class TaskAdminResult(AgencyTask entity, List<AdminMiniResult> admins) : BaseDeletableEntityResult<AgencyTask>(entity, admins)
    {
        public string Title { get; init; } = entity.Title;
        public string? Description { get; init; } = entity.Description;
        public DateTime? DueDate { get; init; } = entity.DueDate;
        public TaskStatus Status { get; init; } = entity.Status;
        public TaskPriority Priority { get; init; } = entity.Priority;
        public Guid? AssignedToId { get; init; } = entity.AssignedToId;
        public Guid? ClientId { get; init; } = entity.ClientId;
        public Guid? ProjectId { get; init; } = entity.ProjectId;
    }
}
