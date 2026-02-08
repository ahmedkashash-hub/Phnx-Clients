using Phnx.Domain.Common;
using Phnx.DTOs.Common;
using Phnx.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.DTOs.Common
{

    public class BaseDeletableEntityResult<T>(T entity, List<AdminMiniResult> miniAdmins) : BaseAuditResult<T>(entity, miniAdmins) where T : BaseDeletableEntity
    {
        public bool IsDeleted => entity.IsDeleted;
        public DateTime? DeletedDate => entity.DeletedDate;
        public string? DeletedBy => entity.DeletedById.HasValue
            ? miniAdmins.FirstOrDefault(x => x.Id == entity.DeletedById)?.Name ?? "-"
            : null;

        public DateTime? RestoredDate => entity.RestoredDate;
        public string? RestoredBy => entity.RestoredById.HasValue
            ? miniAdmins.FirstOrDefault(x => x.Id == entity.RestoredById)?.Name ?? "-"
            : null;
    }
}