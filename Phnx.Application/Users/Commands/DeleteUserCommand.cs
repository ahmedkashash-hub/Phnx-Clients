
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Users.Commands;

public record DeleteUserCommand([FromRoute] Guid Id) : IRequest;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));
    }
}

sealed class DeleteUserCommandHandler(IUnitOfWork unitOfWork, ILanguageService languageService) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<User> userRepository = unitOfWork.GenericRepository<User>();
        User? user = await userRepository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.USER_NOT_FOUND));

        if (user.Email == User.AdminEmail)
            throw new BadRequestException("Cannot delete the admin user.");

        userRepository.Delete(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
