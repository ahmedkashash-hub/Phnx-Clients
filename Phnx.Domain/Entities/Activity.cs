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
           
           
           
            Guid projectId
            )
        {
            Type = type;
            Subject = subject;
            Notes = notes;
            OccurredAt = occurredAt;
           
           
           
            ProjectId = projectId;
          
        }

        public static Activity Create(
            ActivityType type,
            string subject,
            string? notes,
            DateTime occurredAt,
         
           
         
            Guid projectId
         )
            => new Activity(type, subject, notes, occurredAt, projectId);

        public ActivityType Type { get; private set; } = ActivityType.Note;
        public string Subject { get; private set; } = string.Empty;
        public string? Notes { get; private set; }
        public DateTime OccurredAt { get; private set; } = DateTime.Now;
        
       
       
        public Guid ProjectId { get; private set; }
      

        public void Update(
            ActivityType type,
            string subject,
            string? notes,
            DateTime occurredAt,
           
           
          
            Guid projectId
          )
        {
            Type = type;
            Subject = subject;
            Notes = notes;
            OccurredAt = occurredAt;
           
           
          
            ProjectId = projectId;
        
        }
    }
}
