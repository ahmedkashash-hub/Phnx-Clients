using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using Phnx.DTOs.Common;
using Phnx.DTOs.Users;
using Phnx.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.DTOs.Projects
{
    public class ProjectAdminResult(Project entity, List<AdminMiniResult> admins) : BaseDeletableEntityResult<Project>(entity, admins)
    {
        public string projectName { get; init; } = entity.ProjectName;
        public string description { get; init; } = entity.Description;
        public DateTime mvpReleaseDate { get; init; } = entity.MvpReleaseDate;
        public DateTime productionReleaseDate { get; init; } = entity.ProductionReleaseDate;
        public DateTime expiryDate { get; init; } = entity.ExpiryDate;
    }
}

