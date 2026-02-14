using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phnx.Contracts;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Activities.Commands;

public record DeleteActivityCommand([FromRoute] Guid Id) : IRequest;

public class DeleteActivityCommandValidator : AbstractValidator<DeleteActivityCommand>
{
    public DeleteActivityCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.ACTIVITY_ID_REQUIRED));
    }
}

sealed class DeleteActivityCommandHandler(IUnitOfWork unitOfWork, ILanguageService languageService) : IRequestHandler<DeleteActivityCommand>
{
    public async Task Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Activity> repository = unitOfWork.GenericRepository<Activity>();
        Activity activity = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.ACTIVITY_NOT_FOUND));

        repository.Delete(activity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
