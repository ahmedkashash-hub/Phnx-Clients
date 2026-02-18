using FluentValidation;
using Phnx.Contracts;
using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;

namespace Phnx.Application.Invoices.Commands;

public class CreateInvoiceCommand : IRequest
{
    public Guid ClientId { get; set; }
    public Guid? ProjectId { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }


    public string? Notes { get; set; }
}

public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.INVOICE_CLIENT_ID_REQUIRED));

        RuleFor(x => x.IssueDate)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.INVOICE_ISSUE_DATE_REQUIRED));

        RuleFor(x => x.DueDate)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.INVOICE_DUE_DATE_REQUIRED))
            .GreaterThanOrEqualTo(x => x.IssueDate)
            .WithMessage(languageService.GetMessage(LanguageConstants.INVOICE_DUE_DATE_INVALID));

        RuleFor(x => x.Subtotal)
            .GreaterThanOrEqualTo(0)
            .WithMessage(languageService.GetMessage(LanguageConstants.INVOICE_SUBTOTAL_INVALID));

        RuleFor(x => x.Tax)
            .GreaterThanOrEqualTo(0)
            .WithMessage(languageService.GetMessage(LanguageConstants.INVOICE_TAX_INVALID));

        RuleFor(x => x.Total)
            .GreaterThan(0)
            .WithMessage(languageService.GetMessage(LanguageConstants.INVOICE_TOTAL_INVALID));

    
    }
}

sealed class CreateInvoiceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateInvoiceCommand>
{
    public async Task Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Invoice> repository = unitOfWork.GenericRepository<Invoice>();

        Invoice invoice = Invoice.Create(
            request.ClientId,
            
            request.IssueDate,
            request.DueDate,
            request.Subtotal,
          
            request.Total,
          
           
            request.Notes);

        await repository.Create(invoice, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
