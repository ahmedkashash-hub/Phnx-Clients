using Microsoft.AspNetCore.Mvc;
using Phnx.Application.AgencyTasks.Commands;
using Phnx.Application.AgencyTasks.Queries;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Web;

namespace Phnx_Clients.Endpoints
{
    public class AgencyTaskEndpoint : BaseEndpointGroup
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(GroupName)
                .WithTags("Task Endpoints")
                .Get("admin", async (ISender sender, [AsParameters] GetAgencyTasksForAdminQuery query, CancellationToken cancellationToken) => await sender.Send(query, cancellationToken))
                .Post("create", async (ISender sender, CreateAgencyTaskCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Put("update", async (ISender sender, UpdateAgencyTaskCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Delete("delete/{id}", async (ISender sender, [AsParameters] DeleteAgencyTaskCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken));
        }
    }
}
