using Microsoft.Extensions.DependencyInjection;
using Phnx.Domain.Common;
using Phnx.Infrastructure.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace Phnx.Infrastructure.Persistence.Extensions
{

    public static class DependancyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, string connectionString, bool enableSensitiveLogging, bool ignoreModelWarnings)
        {
            services.AddScoped<AuditInterceptor>();
            if (ignoreModelWarnings)
            {
                services.AddDbContext<PhnxDbContext>((sp, options) =>
                {
                    var interceptor = sp.GetRequiredService<AuditInterceptor>();
                    options
                        .UseNpgsql(connectionString)
                        .EnableSensitiveDataLogging(enableSensitiveLogging)
                        .AddInterceptors(interceptor)
                        .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
                });
            }
            else
            {
                services.AddDbContext<PhnxDbContext>((sp, options) =>
                {
                    var interceptor = sp.GetRequiredService<AuditInterceptor>();
                    options.UseNpgsql(connectionString)
                        .EnableSensitiveDataLogging(enableSensitiveLogging)
                        .AddInterceptors(interceptor);
                });
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}