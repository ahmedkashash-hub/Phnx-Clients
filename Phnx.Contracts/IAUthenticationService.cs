using Phnx.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Phnx.Contracts
;

public interface IAUthenticationService
{
    (Guid? id, string role) GetIdFromTokenPrinciple(string token);
    Task<(string token, string refreshToken)> Login(Guid id, string name, Language language, List<Claim>? claims);
}
