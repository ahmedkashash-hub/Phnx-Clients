using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.DTOs.Projects
{

    public class ProjectResult(string projectName,string description,DateTime mvpReleaseDate,DateTime productionReleaseDate,DateTime expiryDate)
    {
        public string ProjectName => projectName;
        public string Description => description;
       
        public DateTime MvpReleaseDate => mvpReleaseDate;
        public DateTime ProductionReleaseDate => productionReleaseDate; 
        public DateTime ExpiryDate => expiryDate;

    }

}
