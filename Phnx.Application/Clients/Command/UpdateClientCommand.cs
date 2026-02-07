using FluentValidation;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
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
        public string Name { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public string? Notes { get; set; }
    }
    public class UpdateClientCommandValidator : AbstractValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator(ILanguageService languageService)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));

            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));
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
                ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));

            client.Update(
                request.Name,
                request.CompanyName,
                request.ExpiryDate,
                request.Notes ?? string.Empty);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}