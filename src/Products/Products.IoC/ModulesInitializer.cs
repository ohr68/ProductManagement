using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Products.Application.Extensions;
using Products.ORM.Extensions;

namespace Products.IoC;

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