using Phnx.Domain.Common;

namespace Phnx.Domain.Entities
{
    public class Visit : BaseDeletableEntity
    {
        private Visit() { }

        private Visit(
            Guid clientId,
            DateTime visitTime,
            string note)
        {
            ClientId = clientId;
            VisitTime = visitTime;
            Note = note;
        }

        public static Visit Create(
            Guid clientId,
            DateTime visitTime,
            string note)
            => new(clientId, visitTime, note);

        public Guid ClientId { get; private set; }
        public DateTime VisitTime { get; private set; } = DateTime.UtcNow;
        public string Note { get; private set; } = string.Empty;

        public void Update(
            Guid clientId,
            DateTime visitTime,
            string note)
        {
            ClientId = clientId;
            VisitTime = visitTime;
            Note = note;
        }
    }
}
