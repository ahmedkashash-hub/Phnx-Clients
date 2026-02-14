using FluentValidation;
using Phnx.Domain.Enums;
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
    public CreateAgencyTaskCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.");
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
            request.AssignedToId,
            request.ClientId,
            request.ProjectId);

        await repository.Create(task, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
