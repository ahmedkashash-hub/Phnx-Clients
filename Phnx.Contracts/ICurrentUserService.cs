
using Phnx.Domain.Enums;


namespace Phnx.Contracts
{

    public interface ICurrentUserService
    {
        Guid Id { get; }
        string Name { get; }
        string Role { get; }
        Language Language { get; }
        List<Permission> Permissions { get; }
        bool HasPermission(Permission permission);
    }
}