using System.Linq.Expressions;
using Phnx.DTOs.Clients;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Wrappers;

namespace Phnx.Application.Clients.Queries;

public record GetClientsForAdminQuery(int PageNum = 1, int PageSize = 20, string? Query = null) : IPagedRequest<ClientAdminResult>;

sealed class GetClientsForAdminQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetClientsForAdminQuery, MultiResponse<ClientAdminResult>>
{
    public async Task<MultiResponse<ClientAdminResult>> Handle(GetClientsForAdminQuery request, CancellationToken cancellationToken)
    {
        IGenericRepository<Client> repository = unitOfWork.GenericRepository<Client>();
        Expression<Func<Client, bool>>? predicate = string.IsNullOrWhiteSpace(request.Query)
            ? null
            : x => x.Name.Contains(request.Query) ||
                   x.CompanyName.Contains(request.Query) ||
                   x.Email.Contains(request.Query);

        (int totalCount, List<Client> clients, List<AdminMiniResult> auditUsers) =
            await repository.GetPaginatedWithAudit(request.PageNum, request.PageSize, predicate, null, true, cancellationToken);

        List<ClientAdminResult> result = [.. clients.Select(x => new ClientAdminResult(x, auditUsers))];
        return new MultiResponse<ClientAdminResult>(result, totalCount, request.PageSize);
    }
}
