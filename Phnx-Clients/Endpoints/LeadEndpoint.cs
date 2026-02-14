using Microsoft.AspNetCore.Mvc;
using Phnx.Application.Leads.Commands;
using Phnx.Application.Leads.Queries;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Web;

namespace Phnx_Clients.Endpoints
{
    public class LeadEndpoint : BaseEndpointGroup
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(GroupName)
                .WithTags("Lead Endpoints")
                .Get("admin", async (ISender sender, [AsParameters] GetLeadsForAdminQuery query, CancellationToken cancellationToken) => await sender.Send(query, cancellationToken))
                .Post("create", async (ISender sender, CreateLeadCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Put("update", async (ISender sender, UpdateLeadCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Delete("delete/{id}", async (ISender sender, [AsParameters] DeleteLeadCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken));
        }
    }
}
