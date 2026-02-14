using Phnx.Domain.Enums;

namespace Phnx.DTOs.Invoices
{
    public class InvoiceResult(
        Guid clientId,
        Guid? projectId,
        DateTime issueDate,
        DateTime dueDate,
        decimal subtotal,
        decimal tax,
        decimal total,
        string currency,
        InvoiceStatus status,
        string? notes)
    {
        public Guid ClientId => clientId;
        public Guid? ProjectId => projectId;
        public DateTime IssueDate => issueDate;
        public DateTime DueDate => dueDate;
        public decimal Subtotal => subtotal;
        public decimal Tax => tax;
        public decimal Total => total;
        public string Currency => currency;
        public InvoiceStatus Status => status;
        public string? Notes => notes;
    }
}
