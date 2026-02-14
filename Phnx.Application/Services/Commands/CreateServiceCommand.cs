using FluentValidation;
using Phoenix.Mediator.Abstractions;

namespace Phnx.Application.Services.Commands;

public class CreateServiceCommand : IRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public decimal? BaseRate { get; set; }
    public bool IsActive { get; set; } = true;
}

public class CreateServiceCommandValidator : AbstractValidator<CreateServiceCommand>
{
    public CreateServiceCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.");
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
            request.Category,
            request.BaseRate,
            request.IsActive);

        await repository.Create(service, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
