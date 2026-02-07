using Airport.Application.Users.Commands;
using Airport.Application.Users.Queries;
using Phnx.Domain.Enums;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Web;

namespace Phnx_Clients.Endpoints
{

    public class UserEndpoints : BaseEndpointGroup
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
                .WithTags("User Endpoints")
                .WithDescription("User Management Endpoints")
                .RequireAuthorization()
                .Post("create", async (ISender sender, HttpContext httpContext, [AsParameters] RequestHeaders requestHeaders, CreateUserCommand command, CancellationToken cancellationToken) =>
                {
                    CheckPermission(httpContext, Permission.UserCreate);
                    return await sender.Send(command, cancellationToken);
                })
                .Put("update", async (ISender sender, HttpContext httpContext, [AsParameters] RequestHeaders requestHeaders, UpdateUserCommand command, CancellationToken cancellationToken) =>
                {
                    CheckPermission(httpContext, Permission.UserUpdate);
                    return await sender.Send(command, cancellationToken);
                })
                .Delete("delete/{id}", async (ISender sender, HttpContext httpContext, [AsParameters] RequestHeaders requestHeaders, [AsParameters] DeleteUserCommand command, CancellationToken cancellationToken) =>
                {
                    CheckPermission(httpContext, Permission.UserDelete);
                    return await sender.Send(command, cancellationToken);
                })
                .Get("admin", async (ISender sender, HttpContext httpContext, [AsParameters] RequestHeaders requestHeaders, [AsParameters] GetUsersForAdminQuery query, CancellationToken cancellationToken) =>
                {
                    CheckPermission(httpContext, Permission.UserView);
                    return await sender.Send(query, cancellationToken);
                });
        }
    }
}