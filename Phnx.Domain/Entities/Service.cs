using Phnx.Domain.Common;

namespace Phnx.Domain.Entities
{
    public class Service : BaseDeletableEntity
    {
        private Service() { }

        private Service(
            string name,
            string? description,
            string? category,
            decimal? baseRate,
            bool isActive)
        {
            Name = name;
            Description = description;
            Category = category;
            BaseRate = baseRate;
            IsActive = isActive;
        }

        public static Service Create(
            string name,
            string? description,
            string? category,
            decimal? baseRate,
            bool isActive)
            => new(name, description, category, baseRate, isActive);

        public string Name { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public string? Category { get; private set; }
        public decimal? BaseRate { get; private set; }
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
            Category = category;
            BaseRate = baseRate;
            IsActive = isActive;
        }
    }
}
