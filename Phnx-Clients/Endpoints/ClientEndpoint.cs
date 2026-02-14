using Microsoft.AspNetCore.Mvc;
using Phnx.Application.Clients.Command;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Web;

namespace Phnx_Clients.Endpoints
{
    public class ClientEndpoint : BaseEndpointGroup
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(GroupName)
                .WithTags("Client Endpoints")
                .Post("create", async (ISender sender, CreateClientCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Put("update", async (ISender sender, UpdateClientCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Delete("delete/{id}", async (ISender sender, [AsParameters] DeleteClientCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken));
        }
    }
}
