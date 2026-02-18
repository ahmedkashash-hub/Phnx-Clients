using Phnx.Domain.Common;
using Phnx.Domain.Enums;

namespace Phnx.Domain.Entities
{
    public class Invoice : BaseDeletableEntity
    {
        private Invoice() { }

        private Invoice(
            Guid clientId,
           
            DateTime issueDate,
            DateTime dueDate,
            decimal subtotal,
          
            decimal total,
           
            
            string? notes)
        {
            ClientId = clientId;
          ;
            IssueDate = issueDate;
            DueDate = dueDate;
            Subtotal = subtotal;
           
            Total = total;
            Currency = "IQD";
         
            Notes = notes;
        }

        public static Invoice Create(
            Guid clientId,
          
            DateTime issueDate,
            DateTime dueDate,
            decimal subtotal,
        
            decimal total,
         
          
            string? notes)
            => new(clientId, issueDate, dueDate, subtotal,  total, notes);

        public Guid ClientId { get; private set; }
      
        public DateTime IssueDate { get; private set; }
        public DateTime DueDate { get; private set; }
        public decimal Subtotal { get; private set; }
      
        public decimal Total { get; private set; }
        public string Currency { get; private set; }


        public string? Notes { get; private set; }

        public void Update(
            Guid clientId,
           
            DateTime issueDate,
            DateTime dueDate,
            decimal subtotal,
           
            decimal total,
           
           
            string? notes)
        {
            ClientId = clientId;
          
            IssueDate = issueDate;
            DueDate = dueDate;
            Subtotal = subtotal;
            
            Total = total;
          
          
            Notes = notes;
        }
    }
}
