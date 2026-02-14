
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.DTOs.Users;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Phnx.Application.Users.Queries;

public record GetUsersForAdminQuery(int PageNum = 1, int PageSize = 20, string? Query = null) : IPagedRequest<UserAdminResult>;
sealed class GetUsersForAdminQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetUsersForAdminQuery, MultiResponse<UserAdminResult>>
{
    public async Task<MultiResponse<UserAdminResult>> Handle(GetUsersForAdminQuery request, CancellationToken cancellationToken)
    {
        var userRepo = unitOfWork.GenericRepository<User>();
        Expression<Func<User, bool>>? predicate = string.IsNullOrEmpty(request.Query) ? null : x => x.Name.Contains(request.Query) || x.Email.Contains(request.Query);
        (int totalCount, List<User> users, List<AdminMiniResult> auditUsers)  = await userRepo.GetPaginatedWithAudit(request.PageNum, request.PageSize, predicate, null, true, cancellationToken);
        List<UserAdminResult> result = [.. users.Select(x => new UserAdminResult(x, auditUsers))];
        return new MultiResponse<UserAdminResult>(result, totalCount, request.PageSize);
    }
}
