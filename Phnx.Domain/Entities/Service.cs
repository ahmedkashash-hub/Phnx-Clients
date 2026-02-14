using Phnx.Domain.Common;

namespace Phnx.Domain.Entities
{
    public class Service : BaseDeletableEntity
    {
        private Service() { }

        private Service(
            string name,
            string? description,
            string? provider,
            decimal? ipAddress,
            bool isActive)
        {
            Name = name;
            Description = description;
            Provider = provider;
            IpAddress = ipAddress;
            IsActive = isActive;
        }

        public static Service Create(
            string name,
            string? description,
            string? provider,
            decimal? ipAddress,
            bool isActive)
            => new Service(name, description, provider, ipAddress, isActive);

        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public string? Provider { get; private set; }
        public decimal? IpAddress { get; private set; }
        public bool IsActive { get; private set; }

        public void Update(
            string name,
            string? description,
            string? category,
            decimal? baseRate,
            bool isActive)
        {
            Name = name;
            Description = description;
            Provider = category;
            IpAddress = baseRate;
            IsActive = isActive;
        }
    }
}
