using System.Linq.Expressions;
using Phnx.DTOs.Services;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Wrappers;

namespace Phnx.Application.Services.Queries;

public record GetServicesForAdminQuery(int PageNum = 1, int PageSize = 20, string? Query = null) : IPagedRequest<ServiceAdminResult>;

sealed class GetServicesForAdminQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetServicesForAdminQuery, MultiResponse<ServiceAdminResult>>
{
    public async Task<MultiResponse<ServiceAdminResult>> Handle(GetServicesForAdminQuery request, CancellationToken cancellationToken)
    {
        IGenericRepository<Service> repository = unitOfWork.GenericRepository<Service>();
        bool hasProviderFilter = decimal.TryParse(request.Query, out decimal providerValue);
        Expression<Func<Service, bool>>? predicate = string.IsNullOrWhiteSpace(request.Query)
            ? null
            : x => x.Name.Contains(request.Query) ||
                   (x.Description != null && x.Description.Contains(request.Query)) ||
                   (x.IpAddress != null && x.IpAddress.Contains(request.Query)) ||
                   (hasProviderFilter && x.Provider == providerValue);

        (int totalCount, List<Service> services, List<AdminMiniResult> auditUsers) =
            await repository.GetPaginatedWithAudit(request.PageNum, request.PageSize, predicate, null, true, cancellationToken);

        List<ServiceAdminResult> result = [.. services.Select(x => new ServiceAdminResult(x, auditUsers))];
        return new MultiResponse<ServiceAdminResult>(result, totalCount, request.PageSize);
    }
}
