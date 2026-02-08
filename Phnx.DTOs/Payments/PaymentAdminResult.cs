using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using Phnx.DTOs.Common;
using Phnx.DTOs.Users;
using Phnx.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.DTOs.Payments
{

    public class PaymentAdminResult(Payment entity, List<AdminMiniResult> admins) : BaseDeletableEntityResult<Payment>(entity, admins)
    {
        public string PaymentType { get; init; } = entity.PaymentType;
        public decimal Amount { get; init; } = entity.Amount;
        public DateTime DueDate { get; init ; } = entity.DueDate;
        public DateTime? PaidDate { get; init; } = entity.PaidDate;
        public PaymentStatus Status { get; init; } = entity.Status;
        public string Method { get; init; } = entity.Method;
        public string? TransactionReference { get; init; } = entity.TransactionReference;


    }
}