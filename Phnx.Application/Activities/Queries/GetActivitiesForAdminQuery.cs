using System.Linq.Expressions;
using Phnx.DTOs.Activities;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Wrappers;

namespace Phnx.Application.Activities.Queries;

public record GetActivitiesForAdminQuery(int PageNum = 1, int PageSize = 20, string? Query = null) : IPagedRequest<ActivityAdminResult>;

sealed class GetActivitiesForAdminQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetActivitiesForAdminQuery, MultiResponse<ActivityAdminResult>>
{
    public async Task<MultiResponse<ActivityAdminResult>> Handle(GetActivitiesForAdminQuery request, CancellationToken cancellationToken)
    {
        IGenericRepository<Activity> repository = unitOfWork.GenericRepository<Activity>();
        Expression<Func<Activity, bool>>? predicate = string.IsNullOrWhiteSpace(request.Query)
            ? null
            : x => x.Subject.Contains(request.Query) || (x.Notes != null && x.Notes.Contains(request.Query));

        (int totalCount, List<Activity> activities, List<AdminMiniResult> auditUsers) =
            await repository.GetPaginatedWithAudit(request.PageNum, request.PageSize, predicate, null, true, cancellationToken);

        List<ActivityAdminResult> result = [.. activities.Select(x => new ActivityAdminResult(x, auditUsers))];
        return new MultiResponse<ActivityAdminResult>(result, totalCount, request.PageSize);
    }
}
