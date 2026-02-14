using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using Phnx.DTOs.Common;
using Phnx.DTOs.Users;

namespace Phnx.DTOs.Opportunities
{
    public class OpportunityAdminResult(Opportunity entity, List<AdminMiniResult> admins) : BaseDeletableEntityResult<Opportunity>(entity, admins)
    {
        public string Name { get; init; } = entity.Name;
        public Guid? ClientId { get; init; } = entity.ClientId;
        public Guid? LeadId { get; init; } = entity.LeadId;
        public decimal Value { get; init; } = entity.Value;
        public int Probability { get; init; } = entity.Probability;
        public DateTime? ExpectedCloseDate { get; init; } = entity.ExpectedCloseDate;
        public OpportunityStage Stage { get; init; } = entity.Stage;
        public string? Notes { get; init; } = entity.Notes;
    }
}
