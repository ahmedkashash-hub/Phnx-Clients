using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.DTOs.Clients
{
  
    public class ClientResult( string name,string  companyName,DateTime expiryDate,string notes)
    {
        public string Name => name;
        public string CompanyName => companyName;
        public DateTime ExpiryDate => expiryDate;
        public  string  Notes => notes;
       
    }

}
