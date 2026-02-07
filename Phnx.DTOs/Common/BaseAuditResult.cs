using Phnx.Domain.Common;
using Phnx.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.DTOs.Common
{

    public class BaseAuditResult<T>(T entity, List<AdminMiniResult> miniAdmins) where T : BaseAuditableEntity
    {
        public Guid Id => entity.Id;

        public DateTime CreatedDate => entity.CreatedDate;
        public DateTime? UpdatedDate => entity.UpdatedDate;

        public string CreatedBy => miniAdmins.FirstOrDefault(x => x.Id == entity.CreatedById)?.Name ?? "-";
        public string UpdatedBy => miniAdmins.FirstOrDefault(x => x.Id == entity.UpdatedById)?.Name ?? "-";
    }
}