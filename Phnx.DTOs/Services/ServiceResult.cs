namespace Phnx.DTOs.Services
{
    public class ServiceResult(
        string name,
        string? description,
        string? category,
        decimal? baseRate,
        bool isActive)
    {
        public string Name => name;
        public string? Description => description;
        public string? Category => category;
        public decimal? BaseRate => baseRate;
        public bool IsActive => isActive;
    }
}
