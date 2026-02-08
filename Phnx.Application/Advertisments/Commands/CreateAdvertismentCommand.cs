
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;

namespace Phnx.Application.Advertisments.Commands;

public class CreateAdvertismentCommand:IRequest
{
    public string? Title { get; set; } 
    public IFormFile Image { get; set; } = default!;
    public string? ActionUrl { get; set; }
}

public class CreateAdvertismentCommandValidator : AbstractValidator<CreateAdvertismentCommand>
{
    public CreateAdvertismentCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Image)
            .NotNull()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));
    }
}
sealed class CreateAdvertismentCommandHandler(IUnitOfWork unitOfWork, IFileService fileService) : IRequestHandler<CreateAdvertismentCommand>
{
    public async Task Handle(CreateAdvertismentCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Advertisement> advertisementRepository = unitOfWork.GenericRepository<Advertisement>();
        string imageUrl = await fileService.SaveFileAsync("advertisement_", request.Image, cancellationToken);
        Advertisement advertisement = Advertisement.Create(request.Title, imageUrl, request.ActionUrl);
        await advertisementRepository.Create(advertisement, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}