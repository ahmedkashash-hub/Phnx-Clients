using System.Linq.Expressions;
using Phnx.DTOs.Invoices;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Wrappers;

namespace Phnx.Application.Invoices.Queries;

public record GetInvoicesForAdminQuery(int PageNum = 1, int PageSize = 20, string? Query = null) : IPagedRequest<InvoiceAdminResult>;

sealed class GetInvoicesForAdminQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetInvoicesForAdminQuery, MultiResponse<InvoiceAdminResult>>
{
    public async Task<MultiResponse<InvoiceAdminResult>> Handle(GetInvoicesForAdminQuery request, CancellationToken cancellationToken)
    {
        IGenericRepository<Invoice> repository = unitOfWork.GenericRepository<Invoice>();
        Expression<Func<Invoice, bool>>? predicate = string.IsNullOrWhiteSpace(request.Query)
            ? null
            : x => x.Currency.Contains(request.Query) || (x.Notes != null && x.Notes.Contains(request.Query));

        (int totalCount, List<Invoice> invoices, List<AdminMiniResult> auditUsers) =
            await repository.GetPaginatedWithAudit(request.PageNum, request.PageSize, predicate, null, true, cancellationToken);

        List<InvoiceAdminResult> result = [.. invoices.Select(x => new InvoiceAdminResult(x, auditUsers))];
        return new MultiResponse<InvoiceAdminResult>(result, totalCount, request.PageSize);
    }
}
