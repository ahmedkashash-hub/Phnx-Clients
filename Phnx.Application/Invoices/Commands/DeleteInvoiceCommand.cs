using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phnx.Contracts;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Invoices.Commands;

public record DeleteInvoiceCommand([FromRoute] Guid Id) : IRequest;

public class DeleteInvoiceCommandValidator : AbstractValidator<DeleteInvoiceCommand>
{
    public DeleteInvoiceCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.INVOICE_ID_REQUIRED));
    }
}

sealed class DeleteInvoiceCommandHandler(IUnitOfWork unitOfWork, ILanguageService languageService) : IRequestHandler<DeleteInvoiceCommand>
{
    public async Task Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Invoice> repository = unitOfWork.GenericRepository<Invoice>();
        Invoice invoice = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.INVOICE_NOT_FOUND));

        repository.Delete(invoice);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
