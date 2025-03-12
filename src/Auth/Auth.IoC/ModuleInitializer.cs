using Auth.Application.Extensions;
using Auth.Jwt.Extensions;
using Auth.ORM.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.IoC;

public static class ModuleInitializer
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration,
        bool isDevelopment = false)
    {
        services
            .AddJwtServices(configuration)
            .AddApplicationLayer()
            .AddPersistenceLayer(configuration, isDevelopment);

        return services;
    }
}