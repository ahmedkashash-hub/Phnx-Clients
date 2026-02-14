using Phnx.Domain.Common;
using Phnx.Domain.Enums;

namespace Phnx.Domain.Entities
{
    public class Lead : BaseDeletableEntity
    {
        private Lead() { }

        private Lead(
            string companyName,
            string contactName,
            string email,
            string phoneNumber,
            ContactMethod preferredContactMethod,
            LeadStatus status,
            string source,
            decimal? expectedValue,
            string title,
            string? notes)
        {
            CompanyName = companyName;
            ContactName = contactName;
            Email = email;
            PhoneNumber = phoneNumber;
            PreferredContactMethod = preferredContactMethod;
            Status = status;
            Source = source;
            ExpectedValue = expectedValue;
            Title = title;
            Notes = notes;
        }

        public static Lead Create(
            string companyName,
            string contactName,
            string email,
            string phoneNumber,
            ContactMethod preferredContactMethod,
            LeadStatus status,
            string source,
            decimal? expectedValue,
            string title,
            string? notes)
            => new Lead(companyName, contactName, email, phoneNumber, preferredContactMethod, status, source, expectedValue, title, notes);

        public string CompanyName { get; private set; } = string.Empty;
        public string ContactName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public ContactMethod PreferredContactMethod { get; private set; } = ContactMethod.Email;
        public LeadStatus Status { get; private set; } = LeadStatus.New;
        public string Source { get; private set; } = string.Empty;
        public decimal? ExpectedValue { get; private set; }
        public string Title { get; private set; }
        public string? Notes { get; private set; }
        public Guid? ConvertedClientId { get; private set; }
        public DateTime? ConvertedDate { get; private set; }

        public void Update(
            string companyName,
            string contactName,
            string email,
            string phoneNumber,
            ContactMethod preferredContactMethod,
            LeadStatus status,
            string source,
            decimal? expectedValue,
            string title,
            string? notes)
        {
            CompanyName = companyName;
            ContactName = contactName;
            Email = email;
            PhoneNumber = phoneNumber;
            PreferredContactMethod = preferredContactMethod;
            Status = status;
            Source = source;
            ExpectedValue = expectedValue;
            Title = title;
            Notes = notes;
        }

        public void Convert(Guid clientId)
        {
            ConvertedClientId = clientId;
            ConvertedDate = DateTime.UtcNow;
            Status = LeadStatus.Converted;
        }
    }
}
