using Phnx.Domain.Enums;

namespace Phnx.DTOs.Leads
{
    public class LeadResult(
        string companyName,
        string contactName,
        string email,
        string phoneNumber,
        ContactMethod preferredContactMethod,
        LeadStatus status,
        string source,
        decimal? expectedValue,
        string? title,
        string? notes,
        Guid? convertedClientId,
        DateTime? convertedDate)
    {
        public string CompanyName => companyName;
        public string ContactName => contactName;
        public string Email => email;
        public string PhoneNumber => phoneNumber;
        public ContactMethod PreferredContactMethod => preferredContactMethod;
        public LeadStatus Status => status;
        public string Source => source;
        public decimal? ExpectedValue => expectedValue;
        public string? Title => title;
        public string? Notes => notes;
        public Guid? ConvertedClientId => convertedClientId;
        public DateTime? ConvertedDate => convertedDate;
    }
}
