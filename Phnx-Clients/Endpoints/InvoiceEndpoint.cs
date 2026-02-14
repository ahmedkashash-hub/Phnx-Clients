using Microsoft.AspNetCore.Mvc;
using Phnx.Application.Invoices.Commands;
using Phnx.Application.Invoices.Queries;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Web;

namespace Phnx_Clients.Endpoints
{
    public class InvoiceEndpoint : BaseEndpointGroup
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(GroupName)
                .WithTags("Invoice Endpoints")
                .Get("admin", async (ISender sender, [AsParameters] GetInvoicesForAdminQuery query, CancellationToken cancellationToken) => await sender.Send(query, cancellationToken))
                .Post("create", async (ISender sender, CreateInvoiceCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Put("update", async (ISender sender, UpdateInvoiceCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Delete("delete/{id}", async (ISender sender, [AsParameters] DeleteInvoiceCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken));
        }
    }
}
