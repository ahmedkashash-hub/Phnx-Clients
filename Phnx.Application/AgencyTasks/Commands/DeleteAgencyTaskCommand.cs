using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phnx.Contracts;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.AgencyTasks.Commands;

public record DeleteAgencyTaskCommand([FromRoute] Guid Id) : IRequest;

public class DeleteAgencyTaskCommandValidator : AbstractValidator<DeleteAgencyTaskCommand>
{
    public DeleteAgencyTaskCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.TASK_ID_REQUIRED));
    }
}

sealed class DeleteAgencyTaskCommandHandler(IUnitOfWork unitOfWork, ILanguageService languageService) : IRequestHandler<DeleteAgencyTaskCommand>
{
    public async Task Handle(DeleteAgencyTaskCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<AgencyTask> repository = unitOfWork.GenericRepository<AgencyTask>();
        AgencyTask task = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.TASK_NOT_FOUND));

        repository.Delete(task);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
