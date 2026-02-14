using FluentValidation;
using Phnx.Domain.Enums;
using Phoenix.Mediator.Abstractions;

namespace Phnx.Application.Opportunities.Commands;

public class CreateOpportunityCommand : IRequest
{
    public string Name { get; set; } = string.Empty;
    public Guid? ClientId { get; set; }
    public Guid? LeadId { get; set; }
    public decimal Value { get; set; }
    public int Probability { get; set; }
    public DateTime? ExpectedCloseDate { get; set; }
    public OpportunityStage Stage { get; set; } = OpportunityStage.Discovery;
    public string? Notes { get; set; }
}

public class CreateOpportunityCommandValidator : AbstractValidator<CreateOpportunityCommand>
{
    public CreateOpportunityCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.");

        RuleFor(x => x.Value)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Value must be >= 0.");

        RuleFor(x => x.Probability)
            .InclusiveBetween(0, 100)
            .WithMessage("Probability must be between 0 and 100.");
    }
}

sealed class CreateOpportunityCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateOpportunityCommand>
{
    public async Task Handle(CreateOpportunityCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Opportunity> repository = unitOfWork.GenericRepository<Opportunity>();

        Opportunity opportunity = Opportunity.Create(
            request.Name,
            request.ClientId,
            request.LeadId,
            request.Value,
            request.Probability,
            request.ExpectedCloseDate,
            request.Stage,
            request.Notes);

        await repository.Create(opportunity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
