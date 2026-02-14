using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using Phnx.DTOs.Common;
using Phnx.DTOs.Users;

namespace Phnx.DTOs.Leads
{
    public class LeadAdminResult(Lead entity, List<AdminMiniResult> admins) : BaseDeletableEntityResult<Lead>(entity, admins)
    {
        public string CompanyName { get; init; } = entity.CompanyName;
        public string ContactName { get; init; } = entity.ContactName;
        public string Email { get; init; } = entity.Email;
        public string PhoneNumber { get; init; } = entity.PhoneNumber;
        public ContactMethod PreferredContactMethod { get; init; } = entity.PreferredContactMethod;
        public LeadStatus Status { get; init; } = entity.Status;
        public string Source { get; init; } = entity.Source;
        public decimal? ExpectedValue { get; init; } = entity.ExpectedValue;
        public string? Title { get; init; } = entity.Title;
        public string? Notes { get; init; } = entity.Notes;
        public Guid? ConvertedClientId { get; init; } = entity.ConvertedClientId;
        public DateTime? ConvertedDate { get; init; } = entity.ConvertedDate;
    }
}
