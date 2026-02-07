using Phnx.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Domain.Common
{

    public abstract class BaseDeletableEntity : BaseAuditableEntity
    {
        public Guid? DeletedById { get; private set; }
        public bool IsDeleted { get; private set; } = false;
        public DateTime? DeletedDate { get; private set; }
        public void AuditDelete(Guid deletedById)
        {
            DeletedById = deletedById;
            IsDeleted = true;
            DeletedDate = DateTime.UtcNow;
        }
        public Guid? RestoredById { get; private set; }
        public DateTime? RestoredDate { get; private set; }
        public void AuditRestore(Guid restoredById)
        {
            RestoredById = restoredById;
            DeletedById = null;
            IsDeleted = false;
            DeletedDate = null;
        }
    }
}
