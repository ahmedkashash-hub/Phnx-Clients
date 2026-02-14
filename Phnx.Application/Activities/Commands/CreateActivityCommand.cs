using FluentValidation;
using Phnx.Domain.Enums;
using Phoenix.Mediator.Abstractions;

namespace Phnx.Application.Activities.Commands;

public class CreateActivityCommand : IRequest
{
    public ActivityType Type { get; set; } = ActivityType.Note;
    public string Subject { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime OccurredAt { get; set; }
    public Guid? ClientId { get; set; }
    public Guid? LeadId { get; set; }
    public Guid? ContactId { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? OwnerId { get; set; }
}

public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
{
    public CreateActivityCommandValidator()
    {
        RuleFor(x => x.Subject)
            .NotEmpty()
            .WithMessage("Subject is required.");

        RuleFor(x => x.OccurredAt)
            .NotEmpty()
            .WithMessage("OccurredAt is required.");
    }
}

sealed class CreateActivityCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateActivityCommand>
{
    public async Task Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Activity> repository = unitOfWork.GenericRepository<Activity>();

        Activity activity = Activity.Create(
            request.Type,
            request.Subject,
            request.Notes,
            request.OccurredAt,
            request.ClientId,
            request.LeadId,
            request.ContactId,
            request.ProjectId,
            request.OwnerId);

        await repository.Create(activity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
