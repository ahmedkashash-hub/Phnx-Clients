using Microsoft.AspNetCore.Mvc;
using Phnx.Application.Contacts.Commands;
using Phnx.Application.Contacts.Queries;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Web;

namespace Phnx_Clients.Endpoints
{
    public class ContactEndpoint : BaseEndpointGroup
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(GroupName)
                .WithTags("Contact Endpoints")
                .Get("admin", async (ISender sender, [AsParameters] GetContactsForAdminQuery query, CancellationToken cancellationToken) => await sender.Send(query, cancellationToken))
                .Post("create", async (ISender sender, CreateContactCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Put("update", async (ISender sender, UpdateContactCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Delete("delete/{id}", async (ISender sender, [AsParameters] DeleteContactCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken));
        }
    }
}
