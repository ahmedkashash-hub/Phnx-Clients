using Phnx.Domain.Common;
using Phnx.Domain.Enums;

namespace Phnx.Domain.Entities
{
    public class Client : BaseDeletableEntity
    {
        private Client() { }

        private Client(
            string name,
            string companyName,
            string location,
            string phoneNumber,
            string email,
            DateTime expiryDate,
            ContactMethod preferredContactMethod,
            
            string? website,
            string? notes)
        {
            Name = name;
            CompanyName = companyName;
            Location = location;
            PhoneNumber = phoneNumber;
            Email = email;
            ExpiryDate = expiryDate;
            PreferredContactMethod = preferredContactMethod;
          
            Website = website;
            Notes = notes;
        }

        public static Client Create(
            string name,
            string companyName,
            string location,
            string phoneNumber,
            string email,
            DateTime expiryDate,
            ContactMethod preferredContactMethod,
      
            string? website,
            string? notes)
            => new(name, companyName, location, phoneNumber, email, expiryDate, preferredContactMethod, website, notes);

        public string Name { get; private set; } = string.Empty;
        public string CompanyName { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string Location { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public DateTime ExpiryDate { get; private set; }
        public ContactMethod PreferredContactMethod { get; private set; } = ContactMethod.Email;
    
        public string? Website { get; private set; }
        public string? Notes { get; private set; }

        public void Update(
            string name,
            string companyName,
            string location,
            string phoneNumber,
            string email,
            DateTime expiryDate,
            ContactMethod preferredContactMethod,
           
            string? website,
            string? notes)
        {
            Name = name;
            CompanyName = companyName;
            Location = location;
            PhoneNumber = phoneNumber;
            Email = email;
            ExpiryDate = expiryDate;
            PreferredContactMethod = preferredContactMethod;
           
            Website = website;
            Notes = notes;
        }
    }
}
