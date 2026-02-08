
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Events.Commands;

public class UpdateEventCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public EventType EventType { get; set; }
    public DateTime Date { get; set; }
    public IFormFileCollection? Images { get; set; }
}

public class UpdateEventCommandValidator : AbstractValidator<UpdateEventCommand>
{
    public UpdateEventCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));
        
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));
        
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));
        
        RuleFor(x => x.EventType)
            .IsInEnum()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));
    }
}

sealed class UpdateEventCommandHandler(IUnitOfWork unitOfWork, IFileService fileService, ILanguageService languageService) : IRequestHandler<UpdateEventCommand>
{
    public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Event> eventRepository = unitOfWork.GenericRepository<Event>();
        Event? eventEntity = await eventRepository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));

        List<string> imageUrls = eventEntity.ImageUrls;
        if (request.Images != null && request.Images.Count > 0)
        {
            if (imageUrls.Count > 0)
            {
                fileService.DeleteFiles(imageUrls);
            }
            imageUrls = await fileService.SaveFilesAsync("event_", request.Images, cancellationToken);
        }

        eventEntity.Update(request.Name, request.Description, request.EventType, request.Date, imageUrls);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
