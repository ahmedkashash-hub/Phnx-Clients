using FluentValidation;
using Phnx.Contracts;
using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Contacts.Commands;

public class UpdateContactCommand : IRequest
{
    public Guid Id { get; set; }
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

public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
{
    public UpdateContactCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.CONTACT_ID_REQUIRED));

        RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.CONTACT_CLIENT_ID_REQUIRED));

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.CONTACT_FIRST_NAME_REQUIRED));

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.CONTACT_LAST_NAME_REQUIRED));

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.CONTACT_EMAIL_REQUIRED))
            .EmailAddress()
            .WithMessage(languageService.GetMessage(LanguageConstants.CONTACT_EMAIL_INVALID));

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.CONTACT_PHONE_REQUIRED));

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.CONTACT_TITLE_REQUIRED));
    }
}

sealed class UpdateContactCommandHandler(IUnitOfWork unitOfWork, ILanguageService languageService) : IRequestHandler<UpdateContactCommand>
{
    public async Task Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Contact> repository = unitOfWork.GenericRepository<Contact>();
        Contact contact = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.CONTACT_NOT_FOUND));

        contact.Update(
            request.ClientId,
            request.FirstName,
            request.LastName,
            request.Email,
            request.PhoneNumber,
            request.PreferredContactMethod,
            request.IsPrimary,
            request.Title!,
            request.Notes);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
