using Phnx.Domain.Common;
using Phnx.Domain.Enums;
using DomainTaskStatus = Phnx.Domain.Enums.TaskStatus;

namespace Phnx.Domain.Entities
{
    public class AgencyTask : BaseDeletableEntity
    {
        private AgencyTask() { }

        private AgencyTask(
            string title,
            string? description,
            DateTime? dueDate,
            DomainTaskStatus status,
            TaskPriority priority,
            Guid assignedToId,
            Guid clientId,
            Guid projectId)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Status = status;
            Priority = priority;
            AssignedToId = assignedToId;
            ClientId = clientId;
            ProjectId = projectId;
        }

        public static AgencyTask Create(
            string title,
            string? description,
            DateTime? dueDate,
            DomainTaskStatus status,
            TaskPriority priority,
            Guid assignedToId,
            Guid clientId,
            Guid projectId)
            => new(title, description, dueDate, status, priority, assignedToId, clientId, projectId);

        public string Title { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public DateTime? DueDate { get; private set; }
        public DomainTaskStatus Status { get; private set; } = DomainTaskStatus.Todo;
        public TaskPriority Priority { get; private set; } = TaskPriority.Medium;
        public Guid AssignedToId { get; private set; }
        public Guid  ClientId { get; private set; }
        public Guid ProjectId { get; private set; }

        public void Update(
            string title,
            string? description,
            DateTime? dueDate,
            DomainTaskStatus status,
            TaskPriority priority,
                Guid assignedToId,
            Guid clientId,
            Guid projectId)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            Status = status;
            Priority = priority;
            AssignedToId = assignedToId;
            ClientId = clientId;
            ProjectId = projectId;
        }
    }
}
