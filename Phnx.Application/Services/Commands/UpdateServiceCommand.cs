using FluentValidation;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Services.Commands;

public class UpdateServiceCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public decimal? BaseRate { get; set; }
    public bool IsActive { get; set; } = true;
}

public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
{
    public UpdateServiceCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.");
    }
}

sealed class UpdateServiceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateServiceCommand>
{
    public async Task Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Service> repository = unitOfWork.GenericRepository<Service>();
        Service service = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Service not found.");

        service.Update(
            request.Name,
            request.Description,
            request.Category,
            request.BaseRate,
            request.IsActive);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
