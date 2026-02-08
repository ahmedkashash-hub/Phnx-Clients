using Phnx.Domain.Entities;
using Phnx.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.DTOs.Users
{
    public class UserAdminResult(User entity, List<AdminMiniResult> users) : BaseAuditResult<User>(entity, users)
    {
        public string Name { get; init; } = entity.Name;
        public string Email { get; init; } = entity.Email;
        public int[] Permissions { get; init; } = entity.Permissions;
        public DateTime? LastLogin { get; init; } = entity.LastLogin;

    }
}