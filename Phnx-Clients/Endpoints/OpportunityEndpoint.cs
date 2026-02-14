using Microsoft.AspNetCore.Mvc;
using Phnx.Application.Opportunities.Commands;
using Phnx.Application.Opportunities.Queries;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Web;

namespace Phnx_Clients.Endpoints
{
    public class OpportunityEndpoint : BaseEndpointGroup
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(GroupName)
                .WithTags("Opportunity Endpoints")
                .Get("admin", async (ISender sender, [AsParameters] GetOpportunitiesForAdminQuery query, CancellationToken cancellationToken) => await sender.Send(query, cancellationToken))
                .Post("create", async (ISender sender, CreateOpportunityCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Put("update", async (ISender sender, UpdateOpportunityCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Delete("delete/{id}", async (ISender sender, [AsParameters] DeleteOpportunityCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken));
        }
    }
}
