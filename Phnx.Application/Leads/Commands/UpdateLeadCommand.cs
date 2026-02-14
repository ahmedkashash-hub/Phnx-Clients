using FluentValidation;
using Phnx.Domain.Enums;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Leads.Commands;

public class UpdateLeadCommand : IRequest
{
    public Guid Id { get; set; }
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

public class UpdateLeadCommandValidator : AbstractValidator<UpdateLeadCommand>
{
    public UpdateLeadCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");

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

sealed class UpdateLeadCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateLeadCommand>
{
    public async Task Handle(UpdateLeadCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Lead> repository = unitOfWork.GenericRepository<Lead>();
        Lead lead = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Lead not found.");

        lead.Update(
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

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
