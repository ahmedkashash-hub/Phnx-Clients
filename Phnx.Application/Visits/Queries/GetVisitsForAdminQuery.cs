using System.Linq.Expressions;
using Phnx.DTOs.Visits;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Wrappers;

namespace Phnx.Application.Visits.Queries;

public record GetVisitsForAdminQuery(int PageNum = 1, int PageSize = 20, string? Query = null) : IPagedRequest<VisitAdminResult>;

sealed class GetVisitsForAdminQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetVisitsForAdminQuery, MultiResponse<VisitAdminResult>>
{
    public async Task<MultiResponse<VisitAdminResult>> Handle(GetVisitsForAdminQuery request, CancellationToken cancellationToken)
    {
        IGenericRepository<Visit> repository = unitOfWork.GenericRepository<Visit>();
        Expression<Func<Visit, bool>>? predicate = string.IsNullOrWhiteSpace(request.Query)
            ? null
            : x => x.Note.Contains(request.Query);

        (int totalCount, List<Visit> visits, List<AdminMiniResult> auditUsers) =
            await repository.GetPaginatedWithAudit(request.PageNum, request.PageSize, predicate, null, true, cancellationToken);

        List<VisitAdminResult> result = [.. visits.Select(x => new VisitAdminResult(x, auditUsers))];
        return new MultiResponse<VisitAdminResult>(result, totalCount, request.PageSize);
    }
}
