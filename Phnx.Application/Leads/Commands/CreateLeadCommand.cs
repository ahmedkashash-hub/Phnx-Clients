using FluentValidation;
using Phnx.Domain.Enums;
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
    public CreateLeadCommandValidator()
    {
        RuleFor(x => x.CompanyName)
            .NotEmpty()
            .WithMessage("CompanyName is required.");

        RuleFor(x => x.ContactName)
            .NotEmpty()
            .WithMessage("ContactName is required.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email format is invalid.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("PhoneNumber is required.");

        RuleFor(x => x.Source)
            .NotEmpty()
            .WithMessage("Source is required.");
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
            request.Title,
            request.Notes);

        await repository.Create(lead, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
