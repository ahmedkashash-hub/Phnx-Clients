using Phnx.Domain.Enums;

namespace Phnx.DTOs.Contacts
{
    public class ContactResult(
        string firstName,
        string lastName,
        string email,
        string phoneNumber,
        Guid clientId,
        ContactMethod preferredContactMethod,
        bool isPrimary,
        string? title,
        string? notes)
    {
        public string FirstName => firstName;
        public string LastName => lastName;
        public string Email => email;
        public string PhoneNumber => phoneNumber;
        public Guid ClientId => clientId;
        public ContactMethod PreferredContactMethod => preferredContactMethod;
        public bool IsPrimary => isPrimary;
        public string? Title => title;
        public string? Notes => notes;
    }
}
