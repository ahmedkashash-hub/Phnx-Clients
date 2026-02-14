using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phnx.Contracts;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Leads.Commands;

public record DeleteLeadCommand([FromRoute] Guid Id) : IRequest;

public class DeleteLeadCommandValidator : AbstractValidator<DeleteLeadCommand>
{
    public DeleteLeadCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.LEAD_ID_REQUIRED));
    }
}

sealed class DeleteLeadCommandHandler(IUnitOfWork unitOfWork, ILanguageService languageService) : IRequestHandler<DeleteLeadCommand>
{
    public async Task Handle(DeleteLeadCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Lead> repository = unitOfWork.GenericRepository<Lead>();
        Lead lead = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.LEAD_NOT_FOUND));

        repository.Delete(lead);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
