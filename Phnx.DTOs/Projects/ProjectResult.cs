using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.DTOs.Projects
{

    public class ProjectResult(
        string projectName,
        string description,
        Guid clientId,
        DateTime mvpReleaseDate,
        DateTime productionReleaseDate,
        DateTime expiryDate,
        ProjectStatus status)
    {
        public string ProjectName => projectName;
        public string Description => description;
        public Guid ClientId => clientId;
        public DateTime MvpReleaseDate => mvpReleaseDate;
        public DateTime ProductionReleaseDate => productionReleaseDate; 
        public DateTime ExpiryDate => expiryDate;
        public ProjectStatus Status => status;

    }

}
