using Microsoft.AspNetCore.Mvc;
using Phnx.Application.Visits.Commands;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Web;

namespace Phnx_Clients.Endpoints
{
    public class VisitEndpoint : BaseEndpointGroup
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(GroupName)
                .WithTags("Visit Endpoints")
                .Post("create", async (ISender sender, CreateVisitCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Put("update", async (ISender sender, UpdateVisitCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Delete("delete/{id}", async (ISender sender, [AsParameters] DeleteVisitCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken));
        }
    }
}
