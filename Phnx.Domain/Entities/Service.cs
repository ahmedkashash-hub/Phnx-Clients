using Phnx.Domain.Common;

namespace Phnx.Domain.Entities
{
    public class Service : BaseDeletableEntity
    {
        private Service() { }

        private Service(
            string name,
            string? description,
            string? ipAddress,
            decimal? provider,
            bool isActive)
        {
            Name = name;
            Description = description;
            IpAddress = ipAddress;
            Provider = provider;
            IsActive = isActive;
        }

        public static Service Create(
            string name,
            string? description,
            string? ipAddress,
            decimal? provider,
            bool isActive)
            => new(name, description, ipAddress, provider, isActive);

        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public string? IpAddress { get; private set; }
        public decimal? Provider { get; private set; }
        public bool IsActive { get; private set; }

        public void Update(
            string name,
            string? description,
            string? ipAddress,
            decimal? provider,
            bool isActive)
        {
            Name = name;
            Description = description;
            IpAddress = ipAddress;
            Provider = provider;
            IsActive = isActive;
        }
    }
}
