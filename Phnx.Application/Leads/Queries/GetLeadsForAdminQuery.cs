using System.Linq.Expressions;
using Phnx.DTOs.Leads;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Wrappers;

namespace Phnx.Application.Leads.Queries;

public record GetLeadsForAdminQuery(int PageNum = 1, int PageSize = 20, string? Query = null) : IPagedRequest<LeadAdminResult>;

sealed class GetLeadsForAdminQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetLeadsForAdminQuery, MultiResponse<LeadAdminResult>>
{
    public async Task<MultiResponse<LeadAdminResult>> Handle(GetLeadsForAdminQuery request, CancellationToken cancellationToken)
    {
        IGenericRepository<Lead> repository = unitOfWork.GenericRepository<Lead>();
        Expression<Func<Lead, bool>>? predicate = string.IsNullOrWhiteSpace(request.Query)
            ? null
            : x => x.CompanyName.Contains(request.Query) ||
                   x.ContactName.Contains(request.Query) ||
                   x.Email.Contains(request.Query) ||
                   x.PhoneNumber.Contains(request.Query) ||
                   x.Source.Contains(request.Query);

        (int totalCount, List<Lead> leads, List<AdminMiniResult> auditUsers) =
            await repository.GetPaginatedWithAudit(request.PageNum, request.PageSize, predicate, null, true, cancellationToken);

        List<LeadAdminResult> result = [.. leads.Select(x => new LeadAdminResult(x, auditUsers))];
        return new MultiResponse<LeadAdminResult>(result, totalCount, request.PageSize);
    }
}
