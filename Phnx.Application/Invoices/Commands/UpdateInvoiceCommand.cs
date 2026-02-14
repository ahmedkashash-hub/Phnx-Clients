using FluentValidation;
using Phnx.Contracts;
using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Invoices.Commands;

public class UpdateInvoiceCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Guid? ProjectId { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
    public string Currency { get; set; } = "USD";
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;
    public string? Notes { get; set; }
}

public class UpdateInvoiceCommandValidator : AbstractValidator<UpdateInvoiceCommand>
{
    public UpdateInvoiceCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.INVOICE_ID_REQUIRED));

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

        RuleFor(x => x.Currency)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.INVOICE_CURRENCY_REQUIRED));
    }
}

sealed class UpdateInvoiceCommandHandler(IUnitOfWork unitOfWork, ILanguageService languageService) : IRequestHandler<UpdateInvoiceCommand>
{
    public async Task Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Invoice> repository = unitOfWork.GenericRepository<Invoice>();
        Invoice invoice = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.INVOICE_NOT_FOUND));

        invoice.Update(
            request.ClientId,
            request.ProjectId,
            request.IssueDate,
            request.DueDate,
            request.Subtotal,
            request.Tax,
            request.Total,
            request.Currency,
            request.Status,
            request.Notes);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
