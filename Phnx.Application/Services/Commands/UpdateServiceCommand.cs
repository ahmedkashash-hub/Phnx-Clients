using FluentValidation;
using Phnx.Contracts;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Services.Commands;

public class UpdateServiceCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? IpAddress { get; set; }
    public decimal? Provider { get; set; }
    public bool IsActive { get; set; } = true;
}

public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
{
    public UpdateServiceCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.SERVICE_ID_REQUIRED));

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.SERVICE_NAME_REQUIRED));
    }
}

sealed class UpdateServiceCommandHandler(IUnitOfWork unitOfWork, ILanguageService languageService) : IRequestHandler<UpdateServiceCommand>
{
    public async Task Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Service> repository = unitOfWork.GenericRepository<Service>();
        Service service = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.SERVICE_NOT_FOUND));

        service.Update(
            request.Name,
            request.Description,
            request.IpAddress,
            request.Provider,
            request.IsActive);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
