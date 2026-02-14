using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Phnx.Contracts;
using Phnx.Infrastructure.Services;
using Phnx.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace Phnx.Infrastructure.Services
{

    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(resourcesManager =>
                new ResourceManager("Phnx.Shared.Resources.Language", typeof(AppConstants).Assembly));

            services.AddSingleton<LanguageManager>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<IFileService, FileService>();
            //services.Configure<FlightApisConfiguration>(configuration.GetSection("FlightApis"));
            //services.AddScoped<IFlightApiClientFactory, FlightApiClientFactory>();
            //services.AddScoped<IFlightApiServiceFactory, FlightApiServiceFactory>();
            //services.RegisterFidsService(configuration);
            return services;
        }
    }
}