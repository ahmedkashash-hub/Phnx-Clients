using System.Linq.Expressions;
using Phnx.DTOs.Payments;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Wrappers;

namespace Phnx.Application.Payments.Queries;

public record GetPaymentsForAdminQuery(int PageNum = 1, int PageSize = 20, string? Query = null) : IPagedRequest<PaymentAdminResult>;

sealed class GetPaymentsForAdminQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetPaymentsForAdminQuery, MultiResponse<PaymentAdminResult>>
{
    public async Task<MultiResponse<PaymentAdminResult>> Handle(GetPaymentsForAdminQuery request, CancellationToken cancellationToken)
    {
        IGenericRepository<Payment> repository = unitOfWork.GenericRepository<Payment>();
        Expression<Func<Payment, bool>>? predicate = string.IsNullOrWhiteSpace(request.Query)
            ? null
            : x => x.PaymentType.Contains(request.Query) ||
                   x.Method.Contains(request.Query) ||
                   (x.TransactionReference != null && x.TransactionReference.Contains(request.Query));

        (int totalCount, List<Payment> payments, List<AdminMiniResult> auditUsers) =
            await repository.GetPaginatedWithAudit(request.PageNum, request.PageSize, predicate, null, true, cancellationToken);

        List<PaymentAdminResult> result = [.. payments.Select(x => new PaymentAdminResult(x, auditUsers))];
        return new MultiResponse<PaymentAdminResult>(result, totalCount, request.PageSize);
    }
}
