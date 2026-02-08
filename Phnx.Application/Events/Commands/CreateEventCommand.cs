
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;

namespace Phnx.Application.Events.Commands;

public class CreateEventCommand : IRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public EventType EventType { get; set; }
    public DateTime Date { get; set; }
    public IFormFileCollection? Images { get; set; }
}

public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator(ILanguageService languageService)
    {
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

sealed class CreateEventCommandHandler(IUnitOfWork unitOfWork, IFileService fileService) : IRequestHandler<CreateEventCommand>
{
    public async Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Event> eventRepository = unitOfWork.GenericRepository<Event>();
        List<string> imageUrls = [];
        
        if (request.Images != null && request.Images.Count > 0)
        {
            imageUrls = await fileService.SaveFilesAsync("event_", request.Images, cancellationToken);
        }
        
        Event eventEntity = Event.Create(request.Name, request.Description, request.EventType, request.Date, imageUrls);
        await eventRepository.Create(eventEntity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
