using System.Linq.Expressions;
using Phnx.DTOs.Projects;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Wrappers;

namespace Phnx.Application.Projects.Queries;

public record GetProjectsForAdminQuery(int PageNum = 1, int PageSize = 20, string? Query = null) : IPagedRequest<ProjectAdminResult>;

sealed class GetProjectsForAdminQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetProjectsForAdminQuery, MultiResponse<ProjectAdminResult>>
{
    public async Task<MultiResponse<ProjectAdminResult>> Handle(GetProjectsForAdminQuery request, CancellationToken cancellationToken)
    {
        IGenericRepository<Project> repository = unitOfWork.GenericRepository<Project>();
        Expression<Func<Project, bool>>? predicate = string.IsNullOrWhiteSpace(request.Query)
            ? null
            : x => x.ProjectName.Contains(request.Query) || (x.Description != null && x.Description.Contains(request.Query));

        (int totalCount, List<Project> projects, List<AdminMiniResult> auditUsers) =
            await repository.GetPaginatedWithAudit(request.PageNum, request.PageSize, predicate, null, true, cancellationToken);

        List<ProjectAdminResult> result = [.. projects.Select(x => new ProjectAdminResult(x, auditUsers))];
        return new MultiResponse<ProjectAdminResult>(result, totalCount, request.PageSize);
    }
}
