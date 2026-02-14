using Phnx.Domain.Common;
using Phnx.Domain.Enums;

namespace Phnx.Domain.Entities
{
    public class Contact : BaseDeletableEntity
    {
        private Contact() { }

        private Contact(
            Guid clientId,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            ContactMethod preferredContactMethod,
            bool isPrimary,
            string title,
            string? notes)
        {
            ClientId = clientId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            PreferredContactMethod = preferredContactMethod;
            IsPrimary = isPrimary;
            Title = title;
            Notes = notes;
        }

        public static Contact Create(
            Guid clientId,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            ContactMethod preferredContactMethod,
            bool isPrimary,
            string title,
            string? notes)
            => new Contact(clientId, firstName, lastName, email, phoneNumber, preferredContactMethod, isPrimary, title, notes);

        public Guid ClientId { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public ContactMethod PreferredContactMethod { get; private set; } = ContactMethod.Email;
        public bool IsPrimary { get; private set; }
        public string Title { get; private set; }
        public string? Notes { get; private set; }

        public void Update(
            Guid clientId,
            string firstName,
            string lastName,
            string email,
            string phoneNumber,
            ContactMethod preferredContactMethod,
            bool isPrimary,
            string title,
            string? notes)
        {
            ClientId = clientId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            PreferredContactMethod = preferredContactMethod;
            IsPrimary = isPrimary;
            Title = title;
            Notes = notes;
        }
    }
}
