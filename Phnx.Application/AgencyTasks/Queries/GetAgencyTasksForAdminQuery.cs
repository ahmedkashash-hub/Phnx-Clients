using System.Linq.Expressions;
using Phnx.DTOs.Tasks;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Wrappers;

namespace Phnx.Application.AgencyTasks.Queries;

public record GetAgencyTasksForAdminQuery(int PageNum = 1, int PageSize = 20, string? Query = null) : IPagedRequest<TaskAdminResult>;

sealed class GetAgencyTasksForAdminQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAgencyTasksForAdminQuery, MultiResponse<TaskAdminResult>>
{
    public async Task<MultiResponse<TaskAdminResult>> Handle(GetAgencyTasksForAdminQuery request, CancellationToken cancellationToken)
    {
        IGenericRepository<AgencyTask> repository = unitOfWork.GenericRepository<AgencyTask>();
        Expression<Func<AgencyTask, bool>>? predicate = string.IsNullOrWhiteSpace(request.Query)
            ? null
            : x => x.Title.Contains(request.Query) || (x.Description != null && x.Description.Contains(request.Query));

        (int totalCount, List<AgencyTask> tasks, List<AdminMiniResult> auditUsers) =
            await repository.GetPaginatedWithAudit(request.PageNum, request.PageSize, predicate, null, true, cancellationToken);

        List<TaskAdminResult> result = [.. tasks.Select(x => new TaskAdminResult(x, auditUsers))];
        return new MultiResponse<TaskAdminResult>(result, totalCount, request.PageSize);
    }
}
