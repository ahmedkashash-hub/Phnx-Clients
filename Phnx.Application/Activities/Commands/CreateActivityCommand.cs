using FluentValidation;
using Phnx.Contracts;
using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;

namespace Phnx.Application.Activities.Commands;

public class CreateActivityCommand : IRequest
{
    public ActivityType Type { get; set; } = ActivityType.Note;
    public string Subject { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public DateTime OccurredAt { get; set; }
   
    public Guid ProjectId { get; set; }
    
}

public class CreateActivityCommandValidator : AbstractValidator<CreateActivityCommand>
{
    public CreateActivityCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Subject)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.ACTIVITY_SUBJECT_REQUIRED));

        RuleFor(x => x.OccurredAt)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.ACTIVITY_OCCURRED_AT_REQUIRED));



        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.PROJECT_ID_REQUIRED));

       
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



            request.ProjectId);
            
        await repository.Create(activity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
