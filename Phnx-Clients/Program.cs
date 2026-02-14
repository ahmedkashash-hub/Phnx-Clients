using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi;
using Phnx.Application;
using Phnx.Infrastructure.Persistence.Database;
using Phnx.Infrastructure.Persistence.Extensions;
using Phnx.Infrastructure.Services;
using Phnx_Clients.Extensions;
using Phoenix.Mediator.Abstractions;
using Phoenix.Mediator.Extensions;
using Phoenix.Mediator.Web;
using Scalar.AspNetCore;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.AddLogging();
builder.Services.AddOpenApi();

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
app.UseCors("cors-policy");
app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();
app.Run();

static List<string> ValidateRequiredNonNullableProperties(Type requestType, JsonElement root)
{
    List<string> errors = [];

    if (root.ValueKind != JsonValueKind.Object)
    {
        errors.Add("Request body must be a JSON object.");
        return errors;
    }

    NullabilityInfoContext nullabilityContext = new();

    foreach (PropertyInfo property in requestType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
    {
        if (!property.CanWrite)
            continue;

        if (!IsRequiredNonNullableProperty(property, nullabilityContext))
            continue;

        string jsonPropertyName = property.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name
            ?? JsonNamingPolicy.CamelCase.ConvertName(property.Name);

        if (!root.TryGetProperty(jsonPropertyName, out JsonElement jsonValue))
        {
            errors.Add($"{property.Name} is required.");
            continue;
        }

        if (jsonValue.ValueKind == JsonValueKind.Null)
        {
            errors.Add($"{property.Name} cannot be null.");
            continue;
        }

        if (property.PropertyType == typeof(string) &&
            jsonValue.ValueKind == JsonValueKind.String &&
            string.IsNullOrWhiteSpace(jsonValue.GetString()))
        {
            errors.Add($"{property.Name} cannot be empty.");
        }
    }

    return errors;
}

static bool IsRequiredNonNullableProperty(PropertyInfo property, NullabilityInfoContext nullabilityContext)
{
    Type propertyType = property.PropertyType;

    if (Nullable.GetUnderlyingType(propertyType) is not null)
        return false;

    if (propertyType.IsValueType)
        return true;

    NullabilityInfo info = nullabilityContext.Create(property);
    return info.ReadState == NullabilityState.NotNull || info.WriteState == NullabilityState.NotNull;
}
