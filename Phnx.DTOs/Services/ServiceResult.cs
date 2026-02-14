namespace Phnx.DTOs.Services
{
    public class ServiceResult(
        string name,
        string? description,
        string? ipAddress,
        decimal? provider,
        bool isActive)
    {
        public string Name => name;
        public string? Description => description;
        public string? IpAddress => ipAddress;
        public decimal? Provider => provider;
        public bool IsActive => isActive;
    }
}
