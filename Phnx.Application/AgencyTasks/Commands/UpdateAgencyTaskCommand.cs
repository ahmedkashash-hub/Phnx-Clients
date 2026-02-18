using FluentValidation;
using Phnx.Contracts;
using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
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
  
    public Guid ProjectId { get; set; }
}

public class UpdateAgencyTaskCommandValidator : AbstractValidator<UpdateAgencyTaskCommand>
{
    public UpdateAgencyTaskCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.TASK_ID_REQUIRED));

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.TASK_TITLE_REQUIRED));


        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.PROJECT_ID_REQUIRED));
    }
}

sealed class UpdateAgencyTaskCommandHandler(IUnitOfWork unitOfWork, ILanguageService languageService) : IRequestHandler<UpdateAgencyTaskCommand>
{
    public async Task Handle(UpdateAgencyTaskCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<AgencyTask> repository = unitOfWork.GenericRepository<AgencyTask>();
        AgencyTask task = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.TASK_NOT_FOUND));

        task.Update(
            request.Title,
            request.Description,
            request.DueDate,
         
            request.ProjectId);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
