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

namespace Phnx.Application.Projects.Commands;

public class CreatePaymentCommand : IRequest
{
    public string PaymentType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public string Method { get; set; } = string.Empty;
    public string? TransactionReference { get; set; }
}
public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.PaymentType)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));
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
            request.TransactionReference);

        await repository.Create(payment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
