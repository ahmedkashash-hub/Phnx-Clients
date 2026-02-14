using Phnx.Domain.Common;
using Phnx.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Domain.Entities
{
  

        public class Visit : BaseDeletableEntity
        {
            private Visit() { }
            private Visit(
                Guid clientId,
                Guid projectId,
                DateTime timeVisit,
                string note)

            {
            Note = note;
            ProjectId= projectId;
            VisitTime = timeVisit;
            ClientId = clientId;
           
        }

            public static Visit Create(
                 Guid clientId,
                 Guid projectId,
                DateTime timeVisit,
                string note)
                 => new (clientId, projectId, timeVisit, note);
               
               

            public Guid ClientId { get; private set; }
            public Guid ProjectId { get; private set; }
        public DateTime VisitTime { get; private set; } = DateTime.Now;
            public string Note { get; private set; }=string.Empty;



        public void Update(
               Guid clientId,
               Guid projectId,
                DateTime timeVisit,
                string note)
            {
                ClientId = clientId;
            ProjectId = projectId;
            VisitTime = timeVisit;
                Note = note;

        }

         
        }
    }
