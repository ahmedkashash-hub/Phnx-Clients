using Phnx.Domain.Common;
using Phnx.Domain.Enums;

namespace Phnx.Domain.Entities
{
    public class Opportunity : BaseDeletableEntity
    {
        private Opportunity() { }

        private Opportunity(
            string name,
            Guid clientId,
            Guid leadId,
            decimal value,
            int probability,
            DateTime? expectedCloseDate,
            OpportunityStage stage,
            string? notes)
        {
            Name = name;
            ClientId = clientId;
            LeadId = leadId;
            Value = value;
            Probability = probability;
            ExpectedCloseDate = expectedCloseDate;
            Stage = stage;
            Notes = notes;
        }

        public static Opportunity Create(
            string name,
            Guid clientId,
            Guid leadId,
            decimal value,
            int probability,
            DateTime? expectedCloseDate,
            OpportunityStage stage,
            string? notes)
            => new Opportunity(name, clientId, leadId, value, probability, expectedCloseDate, stage, notes);

        public string Name { get; private set; } = string.Empty;
        public Guid ClientId { get; private set; }
        public Guid? LeadId { get; private set; }
        public decimal Value { get; private set; }
        public int Probability { get; private set; }
        public DateTime? ExpectedCloseDate { get; private set; }
        public OpportunityStage Stage { get; private set; } = OpportunityStage.Discovery;
        public string? Notes { get; private set; }

        public void Update(
            string name,
            Guid clientId,
            Guid leadId,
            decimal value,
            int probability,
            DateTime? expectedCloseDate,
            OpportunityStage stage,
            string? notes)
        {
            Name = name;
            ClientId = clientId;
            LeadId = leadId;
            Value = value;
            Probability = probability;
            ExpectedCloseDate = expectedCloseDate;
            Stage = stage;
            Notes = notes;
        }
    }
}
