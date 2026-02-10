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
                int  clientId,
                DateTime timeVisit,
                string note)

            {
            Note = note;
            VisitTime = timeVisit;
            ClientId = clientId;
           
        }

            public static Visit Create(
                 int clientId,
                DateTime timeVisit,
                string note)
                 => new (clientId, timeVisit, note);
               
               

            public int ClientId { get; private set; }
            public DateTime VisitTime { get; private set; } = DateTime.Now;
            public string Note { get; private set; }=string.Empty;



        public void Update(
               int clientId,
                DateTime timeVisit,
               
                string note)
            {
                ClientId = clientId;
                VisitTime = timeVisit;
                Note = note;

        }

         
        }
    }
