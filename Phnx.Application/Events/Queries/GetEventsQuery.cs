
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using Phnx.DTOs.Events;
using Phnx.Shared.Extensions;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Wrappers;
using System.Linq.Expressions;

namespace Phnx.Application.Events.Queries;

public record GetEventsQuery(int PageNum, int PageSize, string? Query, EventType? EventType) : IPagedRequest<EventResult>;

sealed class GetEventsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetEventsQuery, MultiResponse<EventResult>>
{
    public async Task<MultiResponse<EventResult>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        IGenericRepository<Event> eventRepository = unitOfWork.GenericRepository<Event>();
        Expression<Func<Event, bool>> predicate =
            string.IsNullOrWhiteSpace(request.Query)
                ? x => !x.IsDeleted
                : x => !x.IsDeleted && (x.Name.Contains(request.Query) || x.Description.Contains(request.Query));
        if (request.EventType.HasValue)
            predicate = predicate.And(x => x.EventType == request.EventType.Value);

        (int totalCount, List<EventResult> result) = await eventRepository.GetPaginated(
            request.PageNum,
            request.PageSize,
            predicate,
            null,
            x=> new EventResult(x.Name,x.Description,x.EventType,x.Date,x.ImageUrls),
            cancellationToken);

        return new MultiResponse<EventResult>(result, totalCount, request.PageSize);
    }
}
