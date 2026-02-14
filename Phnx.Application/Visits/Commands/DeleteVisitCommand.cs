using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Visits.Commands
{
    public class DeleteVisitCommand : IRequest
    {
        [FromRoute]
        public Guid Id { get; set; }
    }

    public class DeleteVisitCommandValidator : AbstractValidator<DeleteVisitCommand>
    {
        public DeleteVisitCommandValidator(ILanguageService languageService)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(languageService.GetMessage(LanguageConstants.Visit_ID_REQUIRED));
        }
    }

    sealed class DeleteVisitCommandHandler(IUnitOfWork unitOfWork, ILanguageService languageService)
        : IRequestHandler<DeleteVisitCommand>
    {
        public async Task Handle(DeleteVisitCommand request, CancellationToken cancellationToken)
        {
            IGenericRepository<Visit> visitRepository = unitOfWork.GenericRepository<Visit>();
            Visit visit = await visitRepository.GetById(request.Id, cancellationToken)
                ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.VISIT_NOT_FOUND));

            visitRepository.Delete(visit);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
