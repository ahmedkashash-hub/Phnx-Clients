using FluentValidation;
using Phnx.Domain.Enums;
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
    public UpdateOpportunityCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id is required.");

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

sealed class UpdateOpportunityCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateOpportunityCommand>
{
    public async Task Handle(UpdateOpportunityCommand request, CancellationToken cancellationToken)
    {
        IGenericRepository<Opportunity> repository = unitOfWork.GenericRepository<Opportunity>();
        Opportunity opportunity = await repository.GetById(request.Id, cancellationToken)
            ?? throw new NotFoundException("Opportunity not found.");

        opportunity.Update(
            request.Name,
            request.ClientId,
            request.LeadId,
            request.Value,
            request.Probability,
            request.ExpectedCloseDate,
            request.Stage,
            request.Notes);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
