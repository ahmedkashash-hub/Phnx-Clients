using Microsoft.AspNetCore.Mvc;
using Phnx.Application.Users.Commands;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Web;

namespace Phnx_Clients.Endpoints
{
    public class UserEndpoints : BaseEndpointGroup
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(GroupName)
                .WithTags("Users Endpoints")
                .Post("create", async (ISender sender, CreateUserCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Put("update", async (ISender sender, UpdateUserCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Delete("delete/{id}", async (ISender sender, [AsParameters] DeleteUserCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken));
        }
    }
}
