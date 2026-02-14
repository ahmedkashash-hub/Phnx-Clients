using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using Phnx.DTOs.Common;
using Phnx.DTOs.Users;

namespace Phnx.DTOs.Contacts
{
    public class ContactAdminResult(Contact entity, List<AdminMiniResult> admins) : BaseDeletableEntityResult<Contact>(entity, admins)
    {
        public Guid ClientId { get; init; } = entity.ClientId;
        public string FirstName { get; init; } = entity.FirstName;
        public string LastName { get; init; } = entity.LastName;
        public string Email { get; init; } = entity.Email;
        public string PhoneNumber { get; init; } = entity.PhoneNumber;
        public ContactMethod PreferredContactMethod { get; init; } = entity.PreferredContactMethod;
        public bool IsPrimary { get; init; } = entity.IsPrimary;
        public string? Title { get; init; } = entity.Title;
        public string? Notes { get; init; } = entity.Notes;
    }
}
