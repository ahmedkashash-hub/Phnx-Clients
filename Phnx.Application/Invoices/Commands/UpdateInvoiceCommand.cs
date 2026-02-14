using FluentValidation;
using Phnx.Domain.Enums;
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
    public UpdateInvoiceCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");

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

sealed class UpdateInvoiceCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateInvoiceCommand>
{
    public async Task Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Invoice> repository = unitOfWork.GenericRepository<Invoice>();
        Invoice invoice = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Invoice not found.");

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
