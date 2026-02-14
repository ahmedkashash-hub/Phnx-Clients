using Microsoft.AspNetCore.Mvc;
using Phnx.Application.Visits.Commands;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Web;

namespace Phnx_Clients.Endpoints
{
    public class VisitEndpoint : BaseEndpointGroup
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(GroupName)
                .WithTags("Visit Endpoints")
                .WithDescription("Visit Management Endpoints")
                .RequireAuthorization()
                .PostMultiPart("create", async (ISender sender, HttpContext httpContext, [AsParameters] RequestHeaders requestHeaders, [FromForm] CreateVisitCommand command, CancellationToken cancellationToken) =>
                {
                    CheckPermission(httpContext, Permission.JobCreate);
                    return await sender.Send(command, cancellationToken);
                })
                .PutMultiPart("update", async (ISender sender, HttpContext httpContext, [AsParameters] RequestHeaders requestHeaders, [FromForm] UpdateVisitCommand command, CancellationToken cancellationToken) =>
                {
                    CheckPermission(httpContext, Permission.JobUpdate);
                    return await sender.Send(command, cancellationToken);
                })
                .Delete("delete/{id}", async (ISender sender, HttpContext httpContext, [AsParameters] RequestHeaders requestHeaders, [AsParameters] DeleteVisitCommand command, CancellationToken cancellationToken) =>
                {
                    CheckPermission(httpContext, Permission.JobDelete);
                    return await sender.Send(command, cancellationToken);
                });
            app.MapGroup(GroupName)
                .WithTags("Visit Endpoints")
                .WithDescription("Visit endpoints");
            // .Get("", async (ISender sender, [AsParameters] RequestHeaders requestHeaders, [AsParameters] GetJobsQuery query, CancellationToken cancellationToken) => await sender.Send(query, cancellationToken));
        }
    }
}

