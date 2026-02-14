using FluentValidation;
using Phnx.Contracts;
using Phnx.Domain.Common;
using Phnx.Domain.Entities;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phnx.Application.Visits.Commands
{

public class CreateVisitCommand : IRequest
{
    public Guid ClientId { get; set; }
    public DateTime VisitTime { get; set; }
    public string Note { get; set; } = string.Empty;
}

public class CreateVisitCommandValidator : AbstractValidator<CreateVisitCommand>
{
    public CreateVisitCommandValidator(
        ILanguageService languageService,
        IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.CLIENT_ID_REQUIRED));

        RuleFor(x => x.VisitTime)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.VISIT_TIME_REQUIRED));
    }
}

    sealed class CreateVisitCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<CreateVisitCommand>
    {
        public async Task Handle(CreateVisitCommand request, CancellationToken cancellationToken)
        {
            IGenericRepository<Visit> visitRepository =
                unitOfWork.GenericRepository<Visit>();

            Visit visit = Visit.Create(
                request.ClientId,
                request.VisitTime,
                request.Note);

            await visitRepository.Create(visit, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
