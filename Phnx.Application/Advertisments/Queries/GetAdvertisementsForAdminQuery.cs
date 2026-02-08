

using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.DTOs.Advertisements;
using Phnx.DTOs.Users;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Wrappers;
using System.Linq.Expressions;

namespace Phnx.Application.Advertisments.Queries;

public record GetAdvertisementsForAdminQuery(int PageNum,int PageSize,string? Query) : IPagedRequest<AdvertisementAdminResult>;

sealed class GetAdvertisementsForAdminQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAdvertisementsForAdminQuery, MultiResponse<AdvertisementAdminResult>>
{
    public async Task<MultiResponse<AdvertisementAdminResult>> Handle(GetAdvertisementsForAdminQuery request, CancellationToken cancellationToken)
    {
        IGenericRepository<Advertisement> advertisementRepository = unitOfWork.GenericRepository<Advertisement>();
        Expression<Func<Advertisement, bool>>? predicate =
            string.IsNullOrWhiteSpace(request.Query)
                ? null
                : x => x.Title!.Contains(request.Query);
        (int totalCount, List<Advertisement> advertisements, List<AdminMiniResult> auditUsers)  = await advertisementRepository.GetPaginatedWithAudit(request.PageNum, request.PageSize, predicate, null, true, cancellationToken);

        Dictionary<Guid, AdminMiniResult> auditUsersDict = auditUsers.ToDictionary(x => x.Id);

        var result = advertisements.Select(ad => new AdvertisementAdminResult(ad, auditUsers)).ToList();
        return new MultiResponse<AdvertisementAdminResult>(result, totalCount, request.PageSize);
    }
}
