using FluentValidation;
using Phnx.Contracts;
using Phnx.Domain.Enums;
using Phnx.Shared.Constants;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Exceptions;

namespace Phnx.Application.Opportunities.Commands;

public class UpdateOpportunityCommand : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? ClientId { get; set; }
    public Guid? LeadId { get; set; }
    public decimal Value { get; set; }
    public int Probability { get; set; }
    public DateTime? ExpectedCloseDate { get; set; }
    public OpportunityStage Stage { get; set; } = OpportunityStage.Discovery;
    public string? Notes { get; set; }
}

public class UpdateOpportunityCommandValidator : AbstractValidator<UpdateOpportunityCommand>
{
    public UpdateOpportunityCommandValidator(ILanguageService languageService)
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.OPPORTUNITY_ID_REQUIRED));

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.OPPORTUNITY_NAME_REQUIRED));

        RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.CLIENT_ID_REQUIRED));

        RuleFor(x => x.LeadId)
            .NotEmpty()
            .WithMessage(languageService.GetMessage(LanguageConstants.LEAD_ID_REQUIRED));

        RuleFor(x => x.Value)
            .GreaterThanOrEqualTo(0)
            .WithMessage(languageService.GetMessage(LanguageConstants.OPPORTUNITY_VALUE_INVALID));

        RuleFor(x => x.Probability)
            .InclusiveBetween(0, 100)
            .WithMessage(languageService.GetMessage(LanguageConstants.OPPORTUNITY_PROBABILITY_INVALID));
    }
}

sealed class UpdateOpportunityCommandHandler(IUnitOfWork unitOfWork, ILanguageService languageService) : IRequestHandler<UpdateOpportunityCommand>
{
    public async Task Handle(UpdateOpportunityCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Opportunity> repository = unitOfWork.GenericRepository<Opportunity>();
        Opportunity opportunity = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException(languageService.GetMessage(LanguageConstants.OPPORTUNITY_NOT_FOUND));

        opportunity.Update(
            request.Name,
            request.ClientId!.Value,
            request.LeadId!.Value,
            request.Value,
            request.Probability,
            request.ExpectedCloseDate,
            request.Stage,
            request.Notes);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
