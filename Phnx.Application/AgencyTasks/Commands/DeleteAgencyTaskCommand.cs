using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.AgencyTasks.Commands;

public record DeleteAgencyTaskCommand([FromRoute] Guid Id) : IRequest;

public class DeleteAgencyTaskCommandValidator : AbstractValidator<DeleteAgencyTaskCommand>
{
    public DeleteAgencyTaskCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
    }
}

sealed class DeleteAgencyTaskCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteAgencyTaskCommand>
{
    public async Task Handle(DeleteAgencyTaskCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<AgencyTask> repository = unitOfWork.GenericRepository<AgencyTask>();
        AgencyTask task = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Task not found.");

        repository.Delete(task);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
