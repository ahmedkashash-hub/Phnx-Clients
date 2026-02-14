using System.Linq.Expressions;
using Phnx.DTOs.Opportunities;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Wrappers;

namespace Phnx.Application.Opportunities.Queries;

public record GetOpportunitiesForAdminQuery(int PageNum = 1, int PageSize = 20, string? Query = null) : IPagedRequest<OpportunityAdminResult>;

sealed class GetOpportunitiesForAdminQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetOpportunitiesForAdminQuery, MultiResponse<OpportunityAdminResult>>
{
    public async Task<MultiResponse<OpportunityAdminResult>> Handle(GetOpportunitiesForAdminQuery request, CancellationToken cancellationToken)
    {
        IGenericRepository<Opportunity> repository = unitOfWork.GenericRepository<Opportunity>();
        Expression<Func<Opportunity, bool>>? predicate = string.IsNullOrWhiteSpace(request.Query)
            ? null
            : x => x.Name.Contains(request.Query) || (x.Notes != null && x.Notes.Contains(request.Query));

        (int totalCount, List<Opportunity> opportunities, List<AdminMiniResult> auditUsers) =
            await repository.GetPaginatedWithAudit(request.PageNum, request.PageSize, predicate, null, true, cancellationToken);

        List<OpportunityAdminResult> result = [.. opportunities.Select(x => new OpportunityAdminResult(x, auditUsers))];
        return new MultiResponse<OpportunityAdminResult>(result, totalCount, request.PageSize);
    }
}
