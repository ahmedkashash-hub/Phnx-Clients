
using FluentValidation;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Airport.Application.Users.Commands;

public class UpdateUserCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int[] Permissions { get; set; } = [];
}

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator(ILanguageService languageService, IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_ID_REQUIRED));
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_NAME_REQUIRED));
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_EMAIL_REQUIRED))
            .EmailAddress()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_EMAIL_INVALID))
            .MustAsync(async (instance, email, cancellation) =>
            {
                if (string.IsNullOrEmpty(email))
                    return false;
                
                IGenericRepository<User> userRepository = unitOfWork.GenericRepository<User>();
                User? existingUser = await userRepository.GetByQuery(x => x.Email == email && x.Id != instance.Id, cancellation);
                return existingUser == null;
            })
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_EMAIL_ALREADY_EXISTS));
        
        RuleFor(x => x.Permissions)
            .Must(permissions =>
            {
                if (permissions == null || permissions.Length == 0)
                    return true; // Permissions are optional, empty array is valid
                
                var validPermissionValues = Enum.GetValues<Permission>().Select(p => (int)p).ToHashSet();
                return permissions.All(p => validPermissionValues.Contains(p));
            })
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_PERMISSIONS_INVALID));
    }
}

sealed class UpdateUserCommandHandler(IUnitOfWork unitOfWork, ILanguageService languageService) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<User> userRepository = unitOfWork.GenericRepository<User>();
        User? user = await userRepository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.USER_NOT_FOUND));

        if (user.Email == User.AdminEmail)
            throw new BadRequestException("Cannot update the admin user.");

        user.Update(request.Name, request.Email, request.Permissions);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
