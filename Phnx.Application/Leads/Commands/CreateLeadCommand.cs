using FluentValidation;
using Phnx.Contracts;
using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;

namespace Phnx.Application.Leads.Commands;

public class CreateLeadCommand : IRequest
{
    public string CompanyName { get; set; } = string.Empty;
    public string ContactName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public ContactMethod PreferredContactMethod { get; set; } = ContactMethod.Email;
    public LeadStatus Status { get; set; } = LeadStatus.New;
    public string Source { get; set; } = string.Empty;
    public decimal? ExpectedValue { get; set; }
    public string? Title { get; set; }
    public string? Notes { get; set; }
}

public class CreateLeadCommandValidator : AbstractValidator<CreateLeadCommand>
{
    public CreateLeadCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.LEAD_COMPANY_NAME_REQUIRED));

        RuleFor(x => x.ContactName)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.LEAD_CONTACT_NAME_REQUIRED));

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.LEAD_EMAIL_REQUIRED))
            .EmailAddress()
            .WithMessage(languageService.GetMessage(LanguageConstants.LEAD_EMAIL_INVALID));

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.LEAD_PHONE_REQUIRED));

        RuleFor(x => x.Source)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.LEAD_SOURCE_REQUIRED));

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.LEAD_TITLE_REQUIRED));
    }
}

sealed class CreateLeadCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateLeadCommand>
{
    public async Task Handle(CreateLeadCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Lead> repository = unitOfWork.GenericRepository<Lead>();

        Lead lead = Lead.Create(
            request.CompanyName,
            request.ContactName,
            request.Email,
            request.PhoneNumber,
            request.PreferredContactMethod,
            request.Status,
            request.Source,
            request.ExpectedValue,
            request.Title!,
            request.Notes);

        await repository.Create(lead, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
