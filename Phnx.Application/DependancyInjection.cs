using Microsoft.Extensions.DependencyInjection;
using Phoenix.Mediator.Mediator;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Phnx.Application
{

    public static class DependancyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediator(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}