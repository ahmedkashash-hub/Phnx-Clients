using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi;
using Phnx.Application;
using Phnx.Infrastructure.Persistence.Database;
using Phnx.Infrastructure.Persistence.Extensions;
using Phnx_Clients.Extensions;
using Phoenix.Mediator.Extensions;
using Phoenix.Mediator.Web;
using Scalar.AspNetCore;
using System.Text.Json;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);
builder.AddLogging();
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((doc, _, _) =>
    {
        doc.Components ??= new OpenApiComponents();
        doc.Components?.SecuritySchemes?["Bearer"] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Enter JWT Bearer token **_only_**"
        };
        return Task.CompletedTask;
    });
});
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);


builder.Services.AddPersistenceServices(connectionString: builder.Configuration.GetConnectionString("Phnx") ?? throw new KeyNotFoundException("dbConnection was not found"),
                                        enableSensitiveLogging: true,
                                        ignoreModelWarnings: true);

builder.Services.AddWebapiServices(builder.Configuration);

var app = builder.Build();
using (IServiceScope scope = app.Services.CreateScope())
{
    var dbcontext = scope.ServiceProvider.GetRequiredService<PhnxDbContext>();
#if DEBUG
    dbcontext.Database.EnsureDeleted();
#endif
    if (dbcontext.Database.GetPendingMigrations().Any())
    {
        dbcontext.Database.Migrate();
    }
}
if (builder.Configuration.GetValue<bool>("isScalarEnabled"))
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
    RequestPath = "/uploads"
});
app.UseRateLimiter();
app.UseHttpsRedirection();
app.UseCors("Allowdashboard");
app.UseAuthentication();
app.UseAuthorization();
app.MapEndpoints();
app.Run();