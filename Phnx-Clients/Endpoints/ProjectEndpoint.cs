using Microsoft.AspNetCore.Mvc;
using Phnx.Application.Projects.Commands;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Web;

namespace Phnx_Clients.Endpoints
{
    public class ProjectEndpoint : BaseEndpointGroup
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(GroupName)
                .WithTags("Project Endpoints")
                .Post("create", async (ISender sender, CreateProjectCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Put("update", async (ISender sender, UpdateProjectCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Delete("delete/{id}", async (ISender sender, [AsParameters] DeleteProjectCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken));
        }
    }
}
