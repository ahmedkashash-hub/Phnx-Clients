using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;
using System.Reflection.Emit;

namespace Phnx.Application.Payments.Commands;

public class CreatePaymentCommand : IRequest
{
    public string PaymentType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public string Method { get; set; } = string.Empty;
    public Guid ClientId { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? InvoiceId { get; set; }
    public string? TransactionReference { get; set; }
}
public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.PaymentType)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.PAYMENT_TYPE_REQUIRED));

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage(languageService.GetMessage(LanguageConstants.PAYMENT_AMOUNT_REQUIRED));

        RuleFor(x => x.DueDate)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.PAYMENT_DUE_DATE_REQUIRED));

        RuleFor(x => x.Method)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.PAYMENT_METHOD_REQUIRED));

        RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.CLIENT_ID_REQUIRED));

        RuleFor(x => x.ProjectId)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.PROJECT_ID_REQUIRED));

        RuleFor(x => x.InvoiceId)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.INVOICE_ID_REQUIRED));
    }
}
sealed class CreatePaymentCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreatePaymentCommand>
{
    public async Task Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Payment> repository = unitOfWork.GenericRepository<Payment>();

        Payment payment = Payment.Create(
            request.PaymentType,
            request.Amount,
            request.DueDate,
            request.Method,
            request.ClientId,
            request.ProjectId!.Value,
            request.InvoiceId!.Value,
            request.TransactionReference);

        await repository.Create(payment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
