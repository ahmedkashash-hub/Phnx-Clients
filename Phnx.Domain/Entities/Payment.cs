using Phnx.Domain.Common;
using Phnx.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Domain.Entities
{

    public class Payment : BaseDeletableEntity
    {
        private Payment() { }

        private Payment(
            string paymentType,
            decimal amount,
            DateTime dueDate,
            string method,
            string? transactionReference)
        {
            PaymentType = paymentType;
            Amount = amount;
            DueDate = dueDate;
            Method = method;
            TransactionReference = transactionReference;
            Status = PaymentStatus.Pending;
        }

        public static Payment Create(
            string paymentType,
            decimal amount,
            DateTime dueDate,
            string method,
            string? transactionReference = null)
            => new(paymentType, amount, dueDate, method, transactionReference);

        public string PaymentType { get; private set; } = string.Empty;
        public decimal Amount { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime? PaidDate { get; private set; }
        public PaymentStatus Status { get; private set; }
        public string Method { get; private set; } = string.Empty;
        public string? TransactionReference { get; private set; }

        public void Update(
            string paymentType,
            decimal amount,
            DateTime dueDate,
            string method,
            string? transactionReference)
        {
            PaymentType = paymentType;
            Amount = amount;
            DueDate = dueDate;
            Method = method;
            TransactionReference = transactionReference;
        }

        public void MarkAsPaid(DateTime paidDate, string? transactionReference = null)
        {
            PaidDate = paidDate;
            TransactionReference = transactionReference ?? TransactionReference;
            Status = PaymentStatus.Paid;
        }

        public void MarkAsOverdue()
        {
            if (Status == PaymentStatus.Pending && DateTime.UtcNow > DueDate)
            {
                Status = PaymentStatus.Overdue;
            }
        }
    }
}