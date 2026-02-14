using Phnx.Domain.Enums;

namespace Phnx.DTOs.Activities
{
    public class ActivityResult(
        ActivityType type,
        string subject,
        string? notes,
        DateTime occurredAt,
        Guid? clientId,
        Guid? leadId,
        Guid? contactId,
        Guid? projectId,
        Guid? ownerId)
    {
        public ActivityType Type => type;
        public string Subject => subject;
        public string? Notes => notes;
        public DateTime OccurredAt => occurredAt;
        public Guid? ClientId => clientId;
        public Guid? LeadId => leadId;
        public Guid? ContactId => contactId;
        public Guid? ProjectId => projectId;
        public Guid? OwnerId => ownerId;
    }
}
