using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phnx.Contracts;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Services.Commands;

public record DeleteServiceCommand([FromRoute] Guid Id) : IRequest;

public class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
{
    public DeleteServiceCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.SERVICE_ID_REQUIRED));
    }
}

sealed class DeleteServiceCommandHandler(IUnitOfWork unitOfWork, ILanguageService languageService) : IRequestHandler<DeleteServiceCommand>
{
    public async Task Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Service> repository = unitOfWork.GenericRepository<Service>();
        Service service = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.SERVICE_NOT_FOUND));

        repository.Delete(service);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
