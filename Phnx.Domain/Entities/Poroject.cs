using Phnx.Domain.Common;
using Phnx.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Domain.Entities
{

    public class Project : BaseDeletableEntity
    {
        private Project() { }

        private Project(
            string projectName,
            string? description,
            int clientId,
            DateTime mvpReleaseDate,
            DateTime productionReleaseDate,
            DateTime expiryDate)
        {
            ProjectName = projectName;
            Description = description;
            CreationDate = DateTime.UtcNow;
            ClientId = clientId;
            MvpReleaseDate = mvpReleaseDate;
            ProductionReleaseDate = productionReleaseDate;
            ExpiryDate = expiryDate;
            Status = ProjectStatus.Planning;
        }

        public static Project Create(
            string projectName,
            string? description,
                int clientId,
            DateTime mvpReleaseDate,
            DateTime productionReleaseDate,
            DateTime expiryDate)
            => new(projectName, description, clientId, mvpReleaseDate, productionReleaseDate, expiryDate);



        public string ProjectName { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public DateTime CreationDate { get; private set; }
        public int ClientId { get; private set; }
        public DateTime MvpReleaseDate { get; private set; }
        public DateTime ProductionReleaseDate { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public ProjectStatus Status { get; private set; }

        public void Update(
            string projectName,
            string? description,
            int clientId,
            DateTime mvpReleaseDate,

            DateTime productionReleaseDate,
            DateTime expiryDate)
        {
            ProjectName = projectName;
            Description = description;
            ClientId = clientId;
            MvpReleaseDate = mvpReleaseDate;
            ProductionReleaseDate = productionReleaseDate;
            ExpiryDate = expiryDate;
        }

        public void ChangeStatus(ProjectStatus status)
        {
            Status = status;
        }
    }
}
