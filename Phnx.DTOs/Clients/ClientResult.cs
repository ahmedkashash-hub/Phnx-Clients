using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.DTOs.Clients
{
  
    public class ClientResult(
        string name,
        string companyName,
        string location,
        string phoneNumber,
        string email,
        DateTime expiryDate,
        ClientStatus status,
        ContactMethod preferredContactMethod,
        string? website,
        string? notes)
    {
        public string Name => name;
        public string CompanyName => companyName;
        public string Location => location;
        public string PhoneNumber => phoneNumber;
        public string Email => email;
        public DateTime ExpiryDate => expiryDate;
        public ClientStatus Status => status;
        public ContactMethod PreferredContactMethod => preferredContactMethod;
        public string? Website => website;
        public string? Notes => notes;
    }

}
