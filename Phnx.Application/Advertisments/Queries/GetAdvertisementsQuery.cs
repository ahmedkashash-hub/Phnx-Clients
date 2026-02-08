
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.DTOs.Advertisements;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Wrappers;

namespace Airport.Application.Advertisments.Queries;

public record GetAdvertisementsQuery : IRequest<SingleResponse<List<AdvertisementResult>>>;

sealed class GetAdvertisementsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAdvertisementsQuery, SingleResponse<List<AdvertisementResult>>>
{
    public async Task<SingleResponse<List<AdvertisementResult>>> Handle(GetAdvertisementsQuery request, CancellationToken cancellationToken)
    {
        IGenericRepository<Advertisement> advertisementRepository = unitOfWork.GenericRepository<Advertisement>();
        List<AdvertisementResult> result = await advertisementRepository.GetAll(x => new AdvertisementResult(x.Title, x.ImageUrl, x.ActionUrl), cancellationToken);
        return new SingleResponse<List<AdvertisementResult>>(result);
    }
}
