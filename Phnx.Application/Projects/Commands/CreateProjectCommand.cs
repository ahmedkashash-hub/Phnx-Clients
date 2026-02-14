using FluentValidation;
using Microsoft.AspNetCore.Http;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;

namespace Phnx.Application.Projects.Commands;

public class CreateProjectCommand : IRequest
{
    public string ProjectName { get; init; } = string.Empty;
    public string? Description { get; init; }
    public Guid ClientId { get; init; }
    public DateTime MvpReleaseDate { get; init; }
    public DateTime ProductionReleaseDate { get; init; }
    public DateTime ExpiryDate { get; init; }
}
public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.ProjectName)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.PROJECT_NAME_REQUIRED));

        RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.CLIENT_ID_REQUIRED));
    }
}
sealed class CreateProjectCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProjectCommand>
{
    public async Task Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Project> repository = unitOfWork.GenericRepository<Project>();

        Project project = Project.Create(
            request.ProjectName,
            request.Description,
            request.ClientId,
            request.MvpReleaseDate,
            request.ProductionReleaseDate,
            request.ExpiryDate);

        await repository.Create(project, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
