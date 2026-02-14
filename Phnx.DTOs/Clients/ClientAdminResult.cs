using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using Phnx.DTOs.Common;
using Phnx.DTOs.Users;
using Phnx.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.DTOs.Clients
{

    public class ClientAdminResult(Client entity, List<AdminMiniResult> admins) : BaseDeletableEntityResult<Client>(entity, admins)
    {
        public string Name { get; init; } = entity.Name;
        public string CompanyName { get; init; } = entity.EntityName;
        public string PhoneNumber { get; init; } = entity.PhoneNumber;
        public string Location { get; init; } = entity.Location;
        public string Email { get; init; } = entity.Email;
        public DateTime ExpiryDate { get; init; } = entity.ExpiryDate;
        public ClientStatus Status { get; init; } = entity.Status;
        public ContactMethod PreferredContactMethod { get; init; } = entity.PreferredContactMethod;
        public string? Website { get; init; } = entity.Website;
        public string? Notes { get; init; } = entity.Notes;
    }

}
