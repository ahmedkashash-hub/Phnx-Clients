using FluentValidation;
using Phnx.Contracts;
using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
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
    
    public Guid ProjectId { get; set; }
  
}

public class UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
{
    public UpdateActivityCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.ACTIVITY_ID_REQUIRED));

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

sealed class UpdateActivityCommandHandler(IUnitOfWork unitOfWork, ILanguageService languageService) : IRequestHandler<UpdateActivityCommand>
{
    public async Task Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Activity> repository = unitOfWork.GenericRepository<Activity>();
        Activity activity = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.ACTIVITY_NOT_FOUND));

        activity.Update(
            request.Type,
            request.Subject,
            request.Notes,
            request.OccurredAt,
          
            request.ProjectId
            );

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
