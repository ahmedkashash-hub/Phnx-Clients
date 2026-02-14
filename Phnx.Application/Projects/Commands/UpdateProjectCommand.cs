using FluentValidation;
using Microsoft.AspNetCore.Http;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Projects.Commands;

public class UpdateProjectCommand : IRequest
{
    public Guid Id { get; init; }
    public string ProjectName { get; init; } = string.Empty;
    public string? Description { get; init; }
    public Guid ClientId { get; init; }
    public DateTime MvpReleaseDate { get; init; }
    public DateTime ProductionReleaseDate { get; init; }
    public DateTime ExpiryDate { get; init; }
}

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.PROJECT_ID_REQUIRED));

        RuleFor(x => x.ProjectName)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.PROJECT_NAME_REQUIRED));

        RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.CLIENT_ID_REQUIRED));
    }
}

sealed class UpdateProjectCommandHandler(
    IUnitOfWork unitOfWork,
    ILanguageService languageService)
    : IRequestHandler<UpdateProjectCommand>
{
    public async Task Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Project> repository = unitOfWork.GenericRepository<Project>();

        Project project = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.PROJECT_NOT_FOUND));

        project.Update(
            request.ProjectName,
            request.Description,
            request.ClientId,

            request.MvpReleaseDate,
            request.ProductionReleaseDate,
            request.ExpiryDate);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
