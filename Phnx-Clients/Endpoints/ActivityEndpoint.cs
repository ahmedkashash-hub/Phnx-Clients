using Microsoft.AspNetCore.Mvc;
using Phnx.Application.Activities.Commands;
using Phnx.Application.Activities.Queries;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Web;

namespace Phnx_Clients.Endpoints
{
    public class ActivityEndpoint : BaseEndpointGroup
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(GroupName)
                .WithTags("Activity Endpoints")
                .Get("admin", async (ISender sender, [AsParameters] GetActivitiesForAdminQuery query, CancellationToken cancellationToken) => await sender.Send(query, cancellationToken))
                .Post("create", async (ISender sender, CreateActivityCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Put("update", async (ISender sender, UpdateActivityCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Delete("delete/{id}", async (ISender sender, [AsParameters] DeleteActivityCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken));
        }
    }
}
