using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Contacts.Commands;

public record DeleteContactCommand([FromRoute] Guid Id) : IRequest;

public class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
{
    public DeleteContactCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
    }
}

sealed class DeleteContactCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteContactCommand>
{
    public async Task Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Contact> repository = unitOfWork.GenericRepository<Contact>();
        Contact contact = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Contact not found.");

        repository.Delete(contact);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
