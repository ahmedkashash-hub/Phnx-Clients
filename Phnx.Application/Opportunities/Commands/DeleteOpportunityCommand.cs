using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phnx.Contracts;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Opportunities.Commands;

public record DeleteOpportunityCommand([FromRoute] Guid Id) : IRequest;

public class DeleteOpportunityCommandValidator : AbstractValidator<DeleteOpportunityCommand>
{
    public DeleteOpportunityCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.OPPORTUNITY_ID_REQUIRED));
    }
}

sealed class DeleteOpportunityCommandHandler(IUnitOfWork unitOfWork, ILanguageService languageService) : IRequestHandler<DeleteOpportunityCommand>
{
    public async Task Handle(DeleteOpportunityCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Opportunity> repository = unitOfWork.GenericRepository<Opportunity>();
        Opportunity opportunity = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.OPPORTUNITY_NOT_FOUND));

        repository.Delete(opportunity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
