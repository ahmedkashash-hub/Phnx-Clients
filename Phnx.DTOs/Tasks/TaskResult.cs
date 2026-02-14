using Phnx.Domain.Enums;
using TaskStatus = Phnx.Domain.Enums.TaskStatus;

namespace Phnx.DTOs.Tasks
{
    public class TaskResult(
        string title,
        string? description,
        DateTime? dueDate,
        TaskStatus status,
        TaskPriority priority,
        Guid? assignedToId,
        Guid? clientId,
        Guid? projectId)
    {
        public string Title => title;
        public string? Description => description;
        public DateTime? DueDate => dueDate;
        public TaskStatus Status => status;
        public TaskPriority Priority => priority;
        public Guid? AssignedToId => assignedToId;
        public Guid? ClientId => clientId;
        public Guid? ProjectId => projectId;
    }
}
