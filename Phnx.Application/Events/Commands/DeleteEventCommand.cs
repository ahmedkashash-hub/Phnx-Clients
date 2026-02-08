
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Events.Commands;

public record DeleteEventCommand([FromRoute] Guid Id) : IRequest;

public class DeleteEventCommandValidator : AbstractValidator<DeleteEventCommand>
{
    public DeleteEventCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));
    }
}

sealed class DeleteEventCommandHandler(IUnitOfWork unitOfWork, IFileService fileService, ILanguageService languageService) : IRequestHandler<DeleteEventCommand>
{
    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Event> eventRepository = unitOfWork.GenericRepository<Event>();
        Event? eventEntity = await eventRepository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));

        if (eventEntity.ImageUrls.Count > 0)
        {
            fileService.DeleteFiles(eventEntity.ImageUrls);
        }
        
        eventRepository.Delete(eventEntity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
