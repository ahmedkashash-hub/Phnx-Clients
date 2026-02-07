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
    public string ProjectName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime MvpReleaseDate { get; set; }
    public DateTime ProductionReleaseDate { get; set; }
    public DateTime ExpiryDate { get; set; }
}
public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectCommandValidator(ILanguageService languageService)
    {
        //RuleFor(x => x.ProjectName)
        //    .NotEmpty()
        //    .WithMessage(languageService.GetMessage(LanguageConstants.PROJECT_NAME_REQUIRED));
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
            request.MvpReleaseDate,
            request.ProductionReleaseDate,
            request.ExpiryDate);

        await repository.Create(project, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
