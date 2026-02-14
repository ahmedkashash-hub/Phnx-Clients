using Phnx.Application.Payments.Commands;
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
                .Post("create", async (ISender sender, CreatePaymentCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Put("update", async (ISender sender, UpdatePaymentCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken))
                .Delete("delete/{id}", async (ISender sender, [AsParameters] DeletePaymentCommand command, CancellationToken cancellationToken) => await sender.Send(command, cancellationToken));
        }
    }
}
