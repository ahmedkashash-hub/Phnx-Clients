using Phnx.Domain.Common;
using Phnx.Domain.Enums;

namespace Phnx.Domain.Entities
{
    public class Invoice : BaseDeletableEntity
    {
        private Invoice() { }

        private Invoice(
            Guid clientId,
            Guid? projectId,
            DateTime issueDate,
            DateTime dueDate,
            decimal subtotal,
          
            decimal total,
            string currency,
            InvoiceStatus status,
            string? notes)
        {
            ClientId = clientId;
            ProjectId = projectId;
            IssueDate = issueDate;
            DueDate = dueDate;
            Subtotal = subtotal;
           
            Total = total;
            Currency = currency;
            Status = status;
            Notes = notes;
        }

        public static Invoice Create(
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
            => new(clientId, projectId, issueDate, dueDate, subtotal,  total, currency, status, notes);

        public Guid ClientId { get; private set; }
        public Guid? ProjectId { get; private set; }
        public DateTime IssueDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public decimal Subtotal { get; private set; }
      
        public decimal Total { get; private set; }
        public string Currency { get; private set; } = "USD";
        public InvoiceStatus Status { get; private set; } = InvoiceStatus.Draft;
        public string? Notes { get; private set; }

        public void Update(
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
            ClientId = clientId;
            ProjectId = projectId;
            IssueDate = issueDate;
            DueDate = dueDate;
            Subtotal = subtotal;
            
            Total = total;
            Currency = currency;
            Status = status;
            Notes = notes;
        }
    }
}
