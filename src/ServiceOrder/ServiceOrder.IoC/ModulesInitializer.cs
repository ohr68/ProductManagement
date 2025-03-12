using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceOrder.Application.Extensions;
using ServiceOrder.ORM.Extensions;

namespace ServiceOrder.IoC;

public static class ModulesInitializer
{ 
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration,
        bool isDevelopment = false)
    {
        services
            .AddApplicationLayer()
            .AddPersistenceLayer(configuration, isDevelopment)
            .AddMessaging(configuration);

        return services;
    }
}