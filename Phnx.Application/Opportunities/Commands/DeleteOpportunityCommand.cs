using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Opportunities.Commands;

public record DeleteOpportunityCommand([FromRoute] Guid Id) : IRequest;

public class DeleteOpportunityCommandValidator : AbstractValidator<DeleteOpportunityCommand>
{
    public DeleteOpportunityCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
    }
}

sealed class DeleteOpportunityCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteOpportunityCommand>
{
    public async Task Handle(DeleteOpportunityCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Opportunity> repository = unitOfWork.GenericRepository<Opportunity>();
        Opportunity opportunity = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Opportunity not found.");

        repository.Delete(opportunity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
