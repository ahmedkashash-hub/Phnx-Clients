using Phnx.Domain.Enums;

namespace Phnx.DTOs.Opportunities
{
    public class OpportunityResult(
        string name,
        Guid? clientId,
        Guid? leadId,
        decimal value,
        int probability,
        DateTime? expectedCloseDate,
        OpportunityStage stage,
        string? notes)
    {
        public string Name => name;
        public Guid? ClientId => clientId;
        public Guid? LeadId => leadId;
        public decimal Value => value;
        public int Probability => probability;
        public DateTime? ExpectedCloseDate => expectedCloseDate;
        public OpportunityStage Stage => stage;
        public string? Notes => notes;
    }
}
