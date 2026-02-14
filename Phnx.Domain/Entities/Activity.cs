using Phnx.Domain.Common;
using Phnx.Domain.Enums;

namespace Phnx.Domain.Entities
{
    public class Activity : BaseDeletableEntity
    {
        private Activity() { }

        private Activity(
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
            Type = type;
            Subject = subject;
            Notes = notes;
            OccurredAt = occurredAt;
            ClientId = clientId;
            LeadId = leadId;
            ContactId = contactId;
            ProjectId = projectId;
            OwnerId = ownerId;
        }

        public static Activity Create(
            ActivityType type,
            string subject,
            string? notes,
            DateTime occurredAt,
            Guid? clientId,
            Guid? leadId,
            Guid? contactId,
            Guid? projectId,
            Guid? ownerId)
            => new Activity(type, subject, notes, occurredAt, clientId, leadId, contactId, projectId, ownerId);

        public ActivityType Type { get; private set; } = ActivityType.Note;
        public string Subject { get; private set; } = string.Empty;
        public string? Notes { get; private set; }
        public DateTime OccurredAt { get; private set; }
        public Guid? ClientId { get; private set; }
        public Guid? LeadId { get; private set; }
        public Guid? ContactId { get; private set; }
        public Guid? ProjectId { get; private set; }
        public Guid? OwnerId { get; private set; }

        public void Update(
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
            Type = type;
            Subject = subject;
            Notes = notes;
            OccurredAt = occurredAt;
            ClientId = clientId;
            LeadId = leadId;
            ContactId = contactId;
            ProjectId = projectId;
            OwnerId = ownerId;
        }
    }
}
