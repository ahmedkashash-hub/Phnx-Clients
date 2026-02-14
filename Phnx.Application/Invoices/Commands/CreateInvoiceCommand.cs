using FluentValidation;
using Phnx.Domain.Enums;
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
    public string Currency { get; set; } = "USD";
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;
    public string? Notes { get; set; }
}

public class CreateInvoiceCommandValidator : AbstractValidator<CreateInvoiceCommand>
{
    public CreateInvoiceCommandValidator()
    {
        RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithMessage("ClientId is required.");

        RuleFor(x => x.IssueDate)
            .NotEmpty()
            .WithMessage("IssueDate is required.");

        RuleFor(x => x.DueDate)
            .NotEmpty()
            .WithMessage("DueDate is required.")
            .GreaterThanOrEqualTo(x => x.IssueDate)
            .WithMessage("DueDate must be on or after IssueDate.");

        RuleFor(x => x.Subtotal)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Subtotal must be >= 0.");

        RuleFor(x => x.Tax)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Tax must be >= 0.");

        RuleFor(x => x.Total)
            .GreaterThan(0)
            .WithMessage("Total must be > 0.");

        RuleFor(x => x.Currency)
            .NotEmpty()
            .WithMessage("Currency is required.");
    }
}

sealed class CreateInvoiceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateInvoiceCommand>
{
    public async Task Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Invoice> repository = unitOfWork.GenericRepository<Invoice>();

        Invoice invoice = Invoice.Create(
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

        await repository.Create(invoice, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
