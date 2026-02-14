using Microsoft.AspNetCore.Mvc;
using Phnx.Application.Payments.Commands;
using Phnx.Application.Payments.Queries;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Web;

namespace Phnx_Clients.Endpoints
{
    public class PaymentEndpoint : BaseEndpointGroup
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(GroupName)
                .WithTags("Payment Endpoints")
                .Get("admin", async (ISender sender, [AsParameters] GetPaymentsForAdminQuery query, CancellationToken cancellationToken) => await sender.Send(query, cancellationToken))
                .Post("create", async (ISender sender, CreatePaymentCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Put("update", async (ISender sender, UpdatePaymentCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Delete("delete/{id}", async (ISender sender, [AsParameters] DeletePaymentCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken));
        }
    }
}
