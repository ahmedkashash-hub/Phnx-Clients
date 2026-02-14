using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using Phnx.DTOs.Common;
using Phnx.DTOs.Users;

namespace Phnx.DTOs.Invoices
{
    public class InvoiceAdminResult(Invoice entity, List<AdminMiniResult> admins) : BaseDeletableEntityResult<Invoice>(entity, admins)
    {
        public Guid ClientId { get; init; } = entity.ClientId;
        public Guid? ProjectId { get; init; } = entity.ProjectId;
        public DateTime IssueDate { get; init; } = entity.IssueDate;
        public DateTime DueDate { get; init; } = entity.DueDate;
        public decimal Subtotal { get; init; } = entity.Subtotal;
        public decimal Tax { get; init; } = entity.Tax;
        public decimal Total { get; init; } = entity.Total;
        public string Currency { get; init; } = entity.Currency;
        public InvoiceStatus Status { get; init; } = entity.Status;
        public string? Notes { get; init; } = entity.Notes;
    }
}
