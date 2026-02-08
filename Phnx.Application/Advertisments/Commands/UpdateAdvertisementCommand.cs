
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Airport.Application.Advertisments.Commands;

public class UpdateAdvertisementCommand : IRequest
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public IFormFile? Image { get; set; }
    public string? ActionUrl { get; set; }
}

public class UpdateAdvertisementCommandValidator : AbstractValidator<UpdateAdvertisementCommand>
{
    public UpdateAdvertisementCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));
    }
}

sealed class UpdateAdvertisementCommandHandler(IUnitOfWork unitOfWork, IFileService fileService, ILanguageService languageService) : IRequestHandler<UpdateAdvertisementCommand>
{
    public async Task Handle(UpdateAdvertisementCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Advertisement> advertisementRepository = unitOfWork.GenericRepository<Advertisement>();
        Advertisement? advertisement = await advertisementRepository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));

        string imageUrl = advertisement.ImageUrl;
        if (request.Image != null)
        {
            if (!string.IsNullOrEmpty(advertisement.ImageUrl))
            {
                fileService.DeleteFile(advertisement.ImageUrl);
            }
            imageUrl = await fileService.SaveFileAsync("advertisement_", request.Image, cancellationToken);
        }

        advertisement.Update(request.Title, imageUrl, request.ActionUrl);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
