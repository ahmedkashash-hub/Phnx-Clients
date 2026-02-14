using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Leads.Commands;

public record DeleteLeadCommand([FromRoute] Guid Id) : IRequest;

public class DeleteLeadCommandValidator : AbstractValidator<DeleteLeadCommand>
{
    public DeleteLeadCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
    }
}

sealed class DeleteLeadCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteLeadCommand>
{
    public async Task Handle(DeleteLeadCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Lead> repository = unitOfWork.GenericRepository<Lead>();
        Lead lead = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Lead not found.");

        repository.Delete(lead);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
