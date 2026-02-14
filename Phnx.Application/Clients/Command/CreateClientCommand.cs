using FluentValidation;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Application.Clients.Command
{
    public class CreateClientCommand : IRequest
    {
        public string Name { get;  set; } =string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string PhoneNumber { get;  set; } = string.Empty;
        public string Location { get;  set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public ContactMethod PreferredContactMethod { get; set; } = ContactMethod.Email;
        public ClientStatus Status { get; set; } = ClientStatus.Active;
        public string? Website { get; set; }
        public string? Notes { get;   set; }

    }
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator(ILanguageService languageService)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(languageService.GetMessage(LanguageConstants.CLIENT_NAME_REQUIRED));
            RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_EMAIL_REQUIRED));

            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage(languageService.GetMessage(LanguageConstants.CLIENT_COMPANTNAME_REQUIRED));

            RuleFor(x => x.ExpiryDate)
                .NotEmpty()
                .WithMessage(languageService.GetMessage(LanguageConstants.CLIENT_EXPIRY_DATE_REQUIRED));
        }
    }


    sealed class CreateClientCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<CreateClientCommand>
    {
        public async Task Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            IGenericRepository<Client> repository = unitOfWork.GenericRepository<Client>();

            Client client = Client.Create(
                request.Name,
                request.CompanyName,
                request.Location,
                request.PhoneNumber,
                request.Email,
                request.ExpiryDate,
                request.PreferredContactMethod,
                request.Status,
                request.Website,
                request.Notes);

            await repository.Create(client, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
