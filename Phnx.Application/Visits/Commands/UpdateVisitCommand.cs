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

    public class UpdateVisitCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public DateTime VisitTime { get; set; }
        public string Note { get; set; } = string.Empty;
    }

    public class UpdateVisitCommandValidator : AbstractValidator<UpdateVisitCommand>
    {
        public UpdateVisitCommandValidator(
            ILanguageService languageService,
            IUnitOfWork unitOfWork)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(languageService.GetMessage(LanguageConstants.Visit_ID_REQUIRED));

            RuleFor(x => x.ClientId)
                .NotEmpty()
                .WithMessage(languageService.GetMessage(LanguageConstants.CLIENT_ID_REQUIRED));
        }
    }

    sealed class UpdateVisitCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateVisitCommand>
    {
        public async Task Handle(UpdateVisitCommand request, CancellationToken cancellationToken)
        {
            IGenericRepository<Visit> visitRepository =
                unitOfWork.GenericRepository<Visit>();

            Visit? visit = await visitRepository.GetById(request.Id, cancellationToken);

            if (visit is null)
                throw new Exception("Visit not found");

            visit.Update(
                request.ClientId,
                request.VisitTime,
                request.Note);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
