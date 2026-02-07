using FluentValidation;
using Microsoft.AspNetCore.Mvc;
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
    public record DeleteClientCommand([FromRoute] Guid Id) : IRequest;
    public class DeleteClientCommandValidator : AbstractValidator<DeleteClientCommand>
    {
        public DeleteClientCommandValidator(ILanguageService languageService)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));
        }
    }
    sealed class DeleteClientCommandHandler(
        IUnitOfWork unitOfWork,
        ILanguageService languageService)
        : IRequestHandler<DeleteClientCommand>
    {
        public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            IGenericRepository<Client> repository = unitOfWork.GenericRepository<Client>();

            Client client = await repository.GetById(request.Id, cancellationToken)
                ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));

            repository.Delete(client);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}