
    using Microsoft.AspNetCore.Mvc;
    using Phnx.Application.Projects.Commands;
    using Phnx.Domain.Enums;
    using Phoenix.Mediator.Abstractions;
    using Phoenix.Mediator.Web;

    namespace Phnx_Clients.Endpoints
    {


        public class ProjectEndpoint : BaseEndpointGroup
        {
            private static void CheckPermission(HttpContext httpContext, Permission requiredPermission)
            {
                if (httpContext.User?.Identity?.IsAuthenticated != true)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    throw new UnauthorizedAccessException("User is not authenticated.");
                }

                var permissionClaims = httpContext.User.FindAll("permission");
                var userPermissions = permissionClaims
                    .Select(c => c.Value)
                    .Where(v => Enum.TryParse<Permission>(v, out _))
                    .Select(v => Enum.Parse<Permission>(v))
                    .ToList();

                if (!userPermissions.Contains(requiredPermission))
                {
                    httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    throw new UnauthorizedAccessException($"User does not have permission: {requiredPermission}");
                }
            }
            public override void Map(WebApplication app)
            {
                app.MapGroup(GroupName)
                    .WithTags("Project Endpoints")
                    .WithDescription("Project Management Endpoints")
                    .RequireAuthorization()
                    .PostMultiPart("create", async (ISender sender, HttpContext httpContext, [AsParameters] RequestHeaders requestHeaders, [FromForm] CreateProjectCommand command, CancellationToken cancellationToken) =>
                    {
                        CheckPermission(httpContext, Permission.JobCreate);
                        return await sender.Send(command, cancellationToken);
                    })
                    .PutMultiPart("update", async (ISender sender, HttpContext httpContext, [AsParameters] RequestHeaders requestHeaders, [FromForm] UpdateProjectCommand command, CancellationToken cancellationToken) =>
                    {
                        CheckPermission(httpContext, Permission.JobUpdate);
                        return await sender.Send(command, cancellationToken);
                    })
                    .Delete("delete/{id}", async (ISender sender, HttpContext httpContext, [AsParameters] RequestHeaders requestHeaders, [AsParameters] DeleteProjectCommand command, CancellationToken cancellationToken) =>
                    {
                        CheckPermission(httpContext, Permission.JobDelete);
                        return await sender.Send(command, cancellationToken);
                    });
                app.MapGroup(GroupName)
                    .WithTags("Project Endpoints")
                    .WithDescription("Project endpoints");
                // .Get("", async (ISender sender, [AsParameters] RequestHeaders requestHeaders, [AsParameters] GetJobsQuery query, CancellationToken cancellationToken) => await sender.Send(query, cancellationToken));
            }
        }
    }
