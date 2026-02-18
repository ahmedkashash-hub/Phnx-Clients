using Phnx.Domain.Common;
using Phnx.Domain.Enums;
using DomainTaskStatus = Phnx.Domain.Enums.TaskStatus;

namespace Phnx.Domain.Entities
{
    public class AgencyTask : BaseDeletableEntity
    {
        private AgencyTask() { }

        private AgencyTask(
            string title,
            string? description,
            DateTime? dueDate,
           
           
            Guid projectId)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
          
           
        
            ProjectId = projectId;
        }

        public static AgencyTask Create(
            string title,
            string? description,
            DateTime? dueDate,
           
          
          
            Guid projectId)
            => new(title, description, dueDate, projectId);

        public string Title { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public DateTime? DueDate { get; private set; }

       
        
        
        public Guid ProjectId { get; private set; }

        public void Update(
            string title,
            string? description,
            DateTime? dueDate,
           
         
               
         
            Guid projectId)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
        
           
          
           
            ProjectId = projectId;
        }
    }
}
