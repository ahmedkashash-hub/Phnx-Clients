using FluentValidation;
using Phnx.Contracts;
using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using DomainTaskStatus = Phnx.Domain.Enums.TaskStatus;
using Phoenix.Mediator.Abstractions;

namespace Phnx.Application.AgencyTasks.Commands;

public class CreateAgencyTaskCommand : IRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public DomainTaskStatus Status { get; set; } = DomainTaskStatus.Todo;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public Guid? AssignedToId { get; set; }
    public Guid? ClientId { get; set; }
    public Guid? ProjectId { get; set; }
}

public class CreateAgencyTaskCommandValidator : AbstractValidator<CreateAgencyTaskCommand>
{
    public CreateAgencyTaskCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.TASK_TITLE_REQUIRED));

        RuleFor(x => x.AssignedToId)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));

        RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.CLIENT_ID_REQUIRED));

        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.PROJECT_ID_REQUIRED));
    }
}

sealed class CreateAgencyTaskCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateAgencyTaskCommand>
{
    public async Task Handle(CreateAgencyTaskCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<AgencyTask> repository = unitOfWork.GenericRepository<AgencyTask>();

        AgencyTask task = AgencyTask.Create(
            request.Title,
            request.Description,
            request.DueDate,
            request.Status,
            request.Priority,
            request.AssignedToId!.Value,
            request.ClientId!.Value,
            request.ProjectId!.Value);

        await repository.Create(task, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
