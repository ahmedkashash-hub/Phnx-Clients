using System.Linq.Expressions;
using Phnx.DTOs.Contacts;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Wrappers;

namespace Phnx.Application.Contacts.Queries;

public record GetContactsForAdminQuery(int PageNum = 1, int PageSize = 20, string? Query = null) : IPagedRequest<ContactAdminResult>;

sealed class GetContactsForAdminQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetContactsForAdminQuery, MultiResponse<ContactAdminResult>>
{
    public async Task<MultiResponse<ContactAdminResult>> Handle(GetContactsForAdminQuery request, CancellationToken cancellationToken)
    {
        IGenericRepository<Contact> repository = unitOfWork.GenericRepository<Contact>();
        Expression<Func<Contact, bool>>? predicate = string.IsNullOrWhiteSpace(request.Query)
            ? null
            : x => x.FirstName.Contains(request.Query) ||
                   x.LastName.Contains(request.Query) ||
                   x.Email.Contains(request.Query) ||
                   x.PhoneNumber.Contains(request.Query);

        (int totalCount, List<Contact> contacts, List<AdminMiniResult> auditUsers) =
            await repository.GetPaginatedWithAudit(request.PageNum, request.PageSize, predicate, null, true, cancellationToken);

        List<ContactAdminResult> result = [.. contacts.Select(x => new ContactAdminResult(x, auditUsers))];
        return new MultiResponse<ContactAdminResult>(result, totalCount, request.PageSize);
    }
}
