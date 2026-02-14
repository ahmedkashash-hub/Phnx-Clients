using Microsoft.AspNetCore.Mvc;
using Phnx.Application.Users.Commands;
using Phnx.Application.Users.Queries;
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
                .Get("admin", async (ISender sender, [AsParameters] GetUsersForAdminQuery query, CancellationToken cancellationToken) => await sender.Send(query, cancellationToken))
                .Post("create", async (ISender sender, CreateUserCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Put("update", async (ISender sender, UpdateUserCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Delete("delete/{id}", async (ISender sender, [AsParameters] DeleteUserCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken));
        }
    }
}
