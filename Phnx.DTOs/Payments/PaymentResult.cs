using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.DTOs.Payments
{
    
    public class PaymentResult(string paymentType, decimal amount, DateTime dueDate,  string method,string transactionReference)
    {
        public string PaymentType => paymentType;
        public decimal Amount => amount;
        public DateTime DueDate => dueDate;
        public string Method => method;
   public string TransactionReference => transactionReference;
    }

}
