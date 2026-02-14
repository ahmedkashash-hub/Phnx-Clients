using FluentValidation;
using Phnx.Domain.Enums;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Activities.Commands;

public class UpdateActivityCommand : IRequest
{
    public Guid Id { get; set; }
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

public class UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
{
    public UpdateActivityCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");

        RuleFor(x => x.Subject)
            .NotEmpty()
            .WithMessage("Subject is required.");

        RuleFor(x => x.OccurredAt)
            .NotEmpty()
            .WithMessage("OccurredAt is required.");
    }
}

sealed class UpdateActivityCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateActivityCommand>
{
    public async Task Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Activity> repository = unitOfWork.GenericRepository<Activity>();
        Activity activity = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Activity not found.");

        activity.Update(
            request.Type,
            request.Subject,
            request.Notes,
            request.OccurredAt,
            request.ClientId,
            request.LeadId,
            request.ContactId,
            request.ProjectId,
            request.OwnerId);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
