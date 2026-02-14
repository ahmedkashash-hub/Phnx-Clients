using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Invoices.Commands;

public record DeleteInvoiceCommand([FromRoute] Guid Id) : IRequest;

public class DeleteInvoiceCommandValidator : AbstractValidator<DeleteInvoiceCommand>
{
    public DeleteInvoiceCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");
    }
}

sealed class DeleteInvoiceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteInvoiceCommand>
{
    public async Task Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Invoice> repository = unitOfWork.GenericRepository<Invoice>();
        Invoice invoice = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Invoice not found.");

        repository.Delete(invoice);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
