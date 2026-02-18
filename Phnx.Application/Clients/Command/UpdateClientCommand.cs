using FluentValidation;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Application.Clients.Command
{
    public class UpdateClientCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get;  set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string PhoneNumber { get;  set; } = string.Empty;
        public string Location { get;  set; } = string.Empty;
        public string Email { get;  set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public ContactMethod PreferredContactMethod { get; set; } = ContactMethod.Email;
      
        public string? Website { get; set; }
        public string? Notes { get;  set; }

    }
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator(ILanguageService languageService)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(languageService.GetMessage(LanguageConstants.CLIENT_ID_REQUIRED));

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(languageService.GetMessage(LanguageConstants.CLIENT_NAME_REQUIRED));

            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage(languageService.GetMessage(LanguageConstants.CLIENT_COMPANYNAME_REQUIRED));
        }
    }
    sealed class UpdateClientCommandHandler(
        IUnitOfWork unitOfWork,
        ILanguageService languageService)
        : IRequestHandler<UpdateClientCommand>
    {
        public async Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            IGenericRepository<Client> repository = unitOfWork.GenericRepository<Client>();

            Client client = await repository.GetById(request.Id, cancellationToken)
                ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.CLIENT_NOT_FOUND));

            client.Update(
                request.Name,
                request.CompanyName,
                request.Location,
                request.PhoneNumber,
                request.Email,
                request.ExpiryDate,
                request.PreferredContactMethod,
              
                request.Website,
                request.Notes);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
