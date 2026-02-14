using FluentValidation;
using Phnx.Contracts;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;

namespace Phnx.Application.Services.Commands;

public class CreateServiceCommand : IRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? IpAddress { get; set; }
    public decimal? Provider { get; set; }
    public bool IsActive { get; set; } = true;
}

public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.SERVICE_NAME_REQUIRED));
    }
}

sealed class CreateServiceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateServiceCommand>
{
    public async Task Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Service> repository = unitOfWork.GenericRepository<Service>();

        Service service = Service.Create(
            request.Name,
            request.Description,
            request.IpAddress,
            request.Provider,
            request.IsActive);

        await repository.Create(service, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
