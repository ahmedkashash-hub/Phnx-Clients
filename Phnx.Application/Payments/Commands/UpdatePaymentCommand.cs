using FluentValidation;
using Microsoft.AspNetCore.Http;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Projects.Commands;

public class UpdatePaymentCommand : IRequest
{
    public Guid Id { get; set; }
    public string PaymentType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime DueDate { get; set; }
    public string Method { get; set; } = string.Empty;
    public string? TransactionReference { get; set; }
}
public class UpdatePaymentCommandValidator : AbstractValidator<UpdatePaymentCommand>
{
    public UpdatePaymentCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));
    }
}
sealed class UpdatePaymentCommandHandler(
    IUnitOfWork unitOfWork,
    ILanguageService languageService)
    : IRequestHandler<UpdatePaymentCommand>
{
    public async Task Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Payment> repository = unitOfWork.GenericRepository<Payment>();

        Payment payment = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));

        payment.Update(
            request.PaymentType,
            request.Amount,
            request.DueDate,
            request.Method,
            request.TransactionReference);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
