using Microsoft.AspNetCore.Mvc;
using Phnx.Application.Services.Commands;
using Phnx.Application.Services.Queries;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Web;

namespace Phnx_Clients.Endpoints
{
    public class ServiceEndpoint : BaseEndpointGroup
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(GroupName)
                .WithTags("Service Endpoints")
                .Get("admin", async (ISender sender, [AsParameters] GetServicesForAdminQuery query, CancellationToken cancellationToken) => await sender.Send(query, cancellationToken))
                .Post("create", async (ISender sender, CreateServiceCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Put("update", async (ISender sender, UpdateServiceCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Delete("delete/{id}", async (ISender sender, [AsParameters] DeleteServiceCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken));
        }
    }
}
