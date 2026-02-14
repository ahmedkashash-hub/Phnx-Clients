using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phnx.Contracts;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Contacts.Commands;

public record DeleteContactCommand([FromRoute] Guid Id) : IRequest;

public class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
{
    public DeleteContactCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.CONTACT_ID_REQUIRED));
    }
}

sealed class DeleteContactCommandHandler(IUnitOfWork unitOfWork, ILanguageService languageService) : IRequestHandler<DeleteContactCommand>
{
    public async Task Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Contact> repository = unitOfWork.GenericRepository<Contact>();
        Contact contact = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.CONTACT_NOT_FOUND));

        repository.Delete(contact);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
