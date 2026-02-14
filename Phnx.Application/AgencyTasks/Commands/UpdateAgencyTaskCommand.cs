using FluentValidation;
using Phnx.Domain.Enums;
using DomainTaskStatus = Phnx.Domain.Enums.TaskStatus;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.AgencyTasks.Commands;

public class UpdateAgencyTaskCommand : IRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public DomainTaskStatus Status { get; set; } = DomainTaskStatus.Todo;
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public Guid? AssignedToId { get; set; }
    public Guid? ClientId { get; set; }
    public Guid? ProjectId { get; set; }
}

public class UpdateAgencyTaskCommandValidator : AbstractValidator<UpdateAgencyTaskCommand>
{
    public UpdateAgencyTaskCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.");
    }
}

sealed class UpdateAgencyTaskCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateAgencyTaskCommand>
{
    public async Task Handle(UpdateAgencyTaskCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<AgencyTask> repository = unitOfWork.GenericRepository<AgencyTask>();
        AgencyTask task = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Task not found.");

        task.Update(
            request.Title,
            request.Description,
            request.DueDate,
            request.Status,
            request.Priority,
            request.AssignedToId,
            request.ClientId,
            request.ProjectId);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
