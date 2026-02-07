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
    public Guid Id { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime MvpReleaseDate { get; set; }
    public DateTime ProductionReleaseDate { get; set; }
    public DateTime ExpiryDate { get; set; }
}

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));

        RuleFor(x => x.ProjectName)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.DEPARTMENT_ID_REQUIRED));
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
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.DEPARTMENT_ID_REQUIRED));

        project.Update(
            request.ProjectName,
            request.Description,
            request.MvpReleaseDate,
            request.ProductionReleaseDate,
            request.ExpiryDate);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}