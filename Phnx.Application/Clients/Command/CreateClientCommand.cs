using FluentValidation;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Application.Clients.Command
{
    public class CreateClientCommand : IRequest
    {
        public string Name { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public string? Notes { get; set; }
    }
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator(ILanguageService languageService)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(languageService.GetMessage(LanguageConstants.CLIENT_NAME_REQUIRED));

            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage(languageService.GetMessage(LanguageConstants.CLIENT_COMPANTNAME_REQUIRED));
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
                request.ExpiryDate,
                request.Notes ?? string.Empty);

            await repository.Create(client, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}