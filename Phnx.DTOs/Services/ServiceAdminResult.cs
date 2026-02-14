using Phnx.Domain.Entities;
using Phnx.DTOs.Common;
using Phnx.DTOs.Users;

namespace Phnx.DTOs.Services
{
    public class ServiceAdminResult(Service entity, List<AdminMiniResult> admins) : BaseDeletableEntityResult<Service>(entity, admins)
    {
        public string Name { get; init; } = entity.Name;
        public string? Description { get; init; } = entity.Description;
        public string? IpAddress { get; init; } = entity.IpAddress;
        public decimal? Provider { get; init; } = entity.Provider;
        public bool IsActive { get; init; } = entity.IsActive;
    }
}
