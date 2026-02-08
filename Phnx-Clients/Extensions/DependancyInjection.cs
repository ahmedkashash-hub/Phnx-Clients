using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Phnx.Contracts;
using Phnx_Clients.Services;
using System.Text;
using System.Threading.RateLimiting;

namespace Phnx_Clients.Extensions
{

    public static class DependancyInjection
    {
        public static IServiceCollection AddWebapiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRateLimiter(options =>
            {
                options.AddPolicy("otpRateLimit", context =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 100,
                            Window = TimeSpan.FromMinutes(3),
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            QueueLimit = 0
                        }));
                options.AddPolicy("loginRateLimit", context =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 100,
                            Window = TimeSpan.FromMinutes(1),
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            QueueLimit = 0
                        }));
                options.AddPolicy("anonymousRateLimit", context =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 20,
                            Window = TimeSpan.FromMinutes(1),
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            QueueLimit = 0
                        }));
                options.OnRejected = (context, cancellationToken) =>
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    return ValueTask.CompletedTask;
                };
            });
            services.AddHttpContextAccessor();
            services.AddTransient<IAuthenticationService, Microsoft.AspNetCore.Authentication.AuthenticationService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? throw new KeyNotFoundException("jwt key was not found!"))),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"] ?? throw new KeyNotFoundException("jwt issuer was not found!"),
                    ValidAudience = configuration["Jwt:Audience"] ?? throw new KeyNotFoundException("jwt audience was not found!"),
                    ClockSkew = TimeSpan.Zero,
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        if (string.IsNullOrEmpty(accessToken))
                        {
                            accessToken = context.Request.Headers.Authorization
                                .FirstOrDefault()
                                ?.Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);
                        }

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            path.StartsWithSegments("/ws/support"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });
            var authorizationBuilder = services.AddAuthorizationBuilder();

            authorizationBuilder.AddPolicy("x-api-key", policy =>
            {
                policy.RequireAssertion(context =>
                {
                    if (context.Resource is not HttpContext httpContext)
                        return false;

                    if (!httpContext.Request.Headers.TryGetValue("X-Api-Key", out var apiKey))
                        return false;
                    return apiKey == configuration["FlightApis:X-Api-Key"];
                });
            });
            services.AddCors(options =>
            {
                options.AddPolicy("cors-policy", policy =>
                {
                    policy
                        .WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>() ?? ["http://localhost.com"])
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
            return services;
        }

    }
}