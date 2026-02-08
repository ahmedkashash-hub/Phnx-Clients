
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Airport.Application.Advertisments.Commands;

public record DeleteAdvertisementCommand([FromRoute] Guid Id) : IRequest;
public class DeleteAdvertisementCommandValidator : AbstractValidator<DeleteAdvertisementCommand>
{
    public DeleteAdvertisementCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));
    }
}

sealed class DeleteAdvertisementCommandHandler(IUnitOfWork unitOfWork, IFileService fileService, ILanguageService languageService) : IRequestHandler<DeleteAdvertisementCommand>
{
    public async Task Handle(DeleteAdvertisementCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Advertisement> advertisementRepository = unitOfWork.GenericRepository<Advertisement>();
        Advertisement? advertisement = await advertisementRepository.GetById(request.Id, cancellationToken)

            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));

        fileService.DeleteFile(advertisement.ImageUrl);
        advertisementRepository.Delete(advertisement);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
