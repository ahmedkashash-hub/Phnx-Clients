
using FluentValidation;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;

namespace Airport.Application.Users.Commands;

public class CreateUserCommand : IRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int[] Permissions { get; set; } = [];
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(ILanguageService languageService, IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_NAME_REQUIRED));
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_EMAIL_REQUIRED))
            .EmailAddress()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_EMAIL_INVALID))
            .MustAsync(async (email, cancellation) =>
            {
                if (string.IsNullOrEmpty(email))
                    return false;
                
                IGenericRepository<User> userRepository = unitOfWork.GenericRepository<User>();
                User? existingUser = await userRepository.GetByQuery(x => x.Email == email, cancellation);
                return existingUser == null;
            })
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_EMAIL_ALREADY_EXISTS));
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.USER_PASSWORD_REQUIRED));
        
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

sealed class CreateUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<User> userRepository = unitOfWork.GenericRepository<User>();
        User user = User.Create(request.Name, request.Email, request.Password, request.Permissions);
        await userRepository.Create(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
