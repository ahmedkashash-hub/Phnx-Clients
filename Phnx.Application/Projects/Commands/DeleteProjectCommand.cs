using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Projects.Commands;

public record DeleteProjectCommand([FromRoute] Guid Id) : IRequest;
public class DeleteProjectCommandValidator : AbstractValidator<DeleteProjectCommand>
{
    public DeleteProjectCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));
    }
}
sealed class DeleteProjectCommandHandler(
    IUnitOfWork unitOfWork,
    ILanguageService languageService)
    : IRequestHandler<DeleteProjectCommand>
{
    public async Task Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Project> repository = unitOfWork.GenericRepository<Project>();

        Project project = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.USER_PASSWORD_REQUIRED));

        repository.Delete(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}