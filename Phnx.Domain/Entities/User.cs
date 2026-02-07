using Phnx.Domain.Common;
using Phnx.Domain.Enums;
using Phnx.Shared.Extensions;
namespace Phnx.Domain.Entities
{

    public class User : BaseAuditableEntity
    {
        public const string AdminEmail = "admin@admin.com";

        public static User Create(string name, string email, string password, int[] permissions) => new()
        {
            Name = name,
            Email = email,
            Password = StringHasher.Hash(password),
            Permissions = permissions
        };
        public static User Seed()
        {
            int[] allPermissions = [.. Enum.GetValues<Permission>().Select(p => (int)p)];

            return new User
            {
                Name = "Admin",
                Email = AdminEmail,
                Password = StringHasher.Hash("123@"),
                Permissions = allPermissions,
                RefreshToken = ""
            };
        }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public int[] Permissions { get; private set; } = [];
        public DateTime? LastLogin { get; private set; }
        public DateTime RefreshTokenExpire { get; private set; }
        public string RefreshToken { get; private set; } = string.Empty;
        public void Login(string refreshToken)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpire = DateTime.Now.AddMonths(2).ToUniversalTime();
        }
        public bool VerifyPassword(string password) => StringHasher.Verify(password, Password);
        public void ResetPassword(string newPassword) => Password = StringHasher.Hash(newPassword);
        public void Update(string name, string email, int[] permissions)
        {
            Name = name;
            Email = email;
            Permissions = permissions;
        }

    }
}