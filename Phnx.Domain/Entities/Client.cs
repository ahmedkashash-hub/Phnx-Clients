using Phnx.Domain.Common;
using Phnx.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Domain.Entities
{

  
    public class Client : BaseDeletableEntity
    {
        private Client() { }

        private Client(
            string name,
            string entityName,
            string location,
            string phoneNumber,
            string email,
            DateTime expiryDate,
            ContactMethod preferredContactMethod,
            ClientStatus status,
            string? website,
            string? notes)
        {
            Name = name;
            this.EntityName = EntityName;
            Location = location;
            PhoneNumber = phoneNumber;
            Email = email;
            ExpiryDate = expiryDate;
            PreferredContactMethod = preferredContactMethod;
            Status = status;
            Website = website;
            Notes = notes;
        }

        public static Client Create(
            string name,
            string entityName,
            string location,
            string phoneNumber,
            string email,
            DateTime expiryDate,
            ContactMethod preferredContactMethod,
            ClientStatus status,
            string? website,
            string? notes)
            => new Client(name, entityName, location, phoneNumber, email, expiryDate, preferredContactMethod, status, website, notes);


        public string Name { get; private set; } = string.Empty;
        public string EntityName { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty;
        public string Location { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public DateTime ExpiryDate { get; private set; }
        public ContactMethod PreferredContactMethod { get; private set; } = ContactMethod.Email;
        public ClientStatus Status { get; private set; } = ClientStatus.Active;
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
            ClientStatus status,
            string? website,
            string? notes)
        {
            Name = name;
            EntityName = companyName;
            Location = location;
            PhoneNumber = phoneNumber;
            Email = email;
            ExpiryDate = expiryDate;
            PreferredContactMethod = preferredContactMethod;
            Status = status;
            Website = website;
            Notes = notes;
        }
    }


}
