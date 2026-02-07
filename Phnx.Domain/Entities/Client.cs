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
                string companyName,
                DateTime expiryDate,
                string notes)
            {
                Name = name;
                CompanyName = companyName;
                RegistrationDate = DateTime.UtcNow;
                ExpiryDate = expiryDate;
                Notes = notes;
                Status = ClientStatus.Active;
            }

            public static Client Create(
                string name,
                string companyName,
                DateTime expiryDate,
                string notes)
                => new(name, companyName, expiryDate, notes);

            public string Name { get; private set; } = string.Empty;
            public string CompanyName { get; private set; } = string.Empty;
            public DateTime RegistrationDate { get; private set; }
            public DateTime ExpiryDate { get; private set; }
            public ClientStatus Status { get; private set; } = ClientStatus.Active;
            public string Notes { get; private set; } = string.Empty;

            public void Update(
                string name,
                string companyName,
                DateTime expiryDate,
                string notes)
            {
                Name = name;
                CompanyName = companyName;
                ExpiryDate = expiryDate;
                Notes = notes;
            }

            public void ChangeStatus(ClientStatus status)
            {
                Status = status;
            }
        }


    }
