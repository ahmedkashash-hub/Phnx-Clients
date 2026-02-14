using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Activities.Commands;

public record DeleteActivityCommand([FromRoute] Guid Id) : IRequest;

public class DeleteActivityCommandValidator : AbstractValidator<DeleteActivityCommand>
{
    public DeleteActivityCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
    }
}

sealed class DeleteActivityCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteActivityCommand>
{
    public async Task Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Activity> repository = unitOfWork.GenericRepository<Activity>();
        Activity activity = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Activity not found.");

        repository.Delete(activity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
