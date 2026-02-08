
using Aiport.DTOs.Events;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.DTOs.Users;
using Phnx.Shared.Extensions;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Wrappers;
using System.Linq.Expressions;

namespace Phnx.Application.Events.Queries;

public record GetAdminEventsQuery(int PageNum, int PageSize, string? Query, bool? IsDeleted) : IPagedRequest<EventAdminResult>;

sealed class GetAdminEventsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAdminEventsQuery, MultiResponse<EventAdminResult>>
{
    public async Task<MultiResponse<EventAdminResult>> Handle(GetAdminEventsQuery request, CancellationToken cancellationToken)
    {
        IGenericRepository<Event> eventRepository = unitOfWork.GenericRepository<Event>();
        Expression<Func<Event, bool>>? predicate = request.IsDeleted == null ? null : x => x.IsDeleted == request.IsDeleted.Value;
        if (!string.IsNullOrWhiteSpace(request.Query))
            predicate = predicate == null
                ? x => x.Name.Contains(request.Query) || x.Description.Contains(request.Query)
                : predicate.And(x => x.Name.Contains(request.Query) || x.Description.Contains(request.Query));

        (int totalCount, List<Event> events, List<AdminMiniResult> auditUsers) = await eventRepository.GetPaginatedWithAudit(
            request.PageNum,
            request.PageSize,
            predicate,
            null,
            true,
            cancellationToken);

        var result = events.Select(evt => new EventAdminResult(evt, auditUsers)).ToList();
        return new MultiResponse<EventAdminResult>(result, totalCount, request.PageSize);
    }
}
