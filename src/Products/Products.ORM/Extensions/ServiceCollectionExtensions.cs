using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Products.Domain.Entities;
using Products.Domain.Interfaces;
using Products.ORM.Context;
using Products.ORM.Repositories;

namespace Products.ORM.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration,
        bool isDevelopment = false)
    {
        services
            .AddDatabase(configuration, isDevelopment)
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration,
        bool isDevelopment)
    {
        services.AddDbContext<ProductsDbContext>(
            options =>
            {
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Testing")
                {
                    // In-memory database for testing
                    options.UseInMemoryDatabase("InMemoryDbForTesting");

                    return;
                }

                var connectionString = configuration.GetConnectionString("Products")!;
                if (!isDevelopment)
                {
                    var password = Environment.GetEnvironmentVariable("SA_PASSWORD");
                    connectionString = string.Format(connectionString, password);
                }

                options.UseNpgsql(connectionString);
            });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Product>, ProductRepository>();
        
        return services;
    }
}