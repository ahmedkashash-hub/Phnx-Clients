using FluentValidation;
using Phnx.Domain.Enums;
using Phoenix.Mediator.Abstractions;

namespace Phnx.Application.Contacts.Commands;

public class CreateContactCommand : IRequest
{
    public Guid ClientId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public ContactMethod PreferredContactMethod { get; set; } = ContactMethod.Email;
    public bool IsPrimary { get; set; }
    public string? Title { get; set; }
    public string? Notes { get; set; }
}

public class CreateContactCommandValidator : AbstractValidator<CreateContactCommand>
{
    public CreateContactCommandValidator()
    {
        RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithMessage("ClientId is required.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("FirstName is required.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("LastName is required.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email format is invalid.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("PhoneNumber is required.");
    }
}

sealed class CreateContactCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateContactCommand>
{
    public async Task Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Contact> repository = unitOfWork.GenericRepository<Contact>();

        Contact contact = Contact.Create(
            request.ClientId,
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.PreferredContactMethod,
            request.IsPrimary,
            request.Title,
            request.Notes);

        await repository.Create(contact, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
