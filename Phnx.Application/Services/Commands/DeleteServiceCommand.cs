using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Services.Commands;

public record DeleteServiceCommand([FromRoute] Guid Id) : IRequest;

public class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
{
    public DeleteServiceCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
    }
}

sealed class DeleteServiceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteServiceCommand>
{
    public async Task Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Service> repository = unitOfWork.GenericRepository<Service>();
        Service service = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Service not found.");

        repository.Delete(service);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
