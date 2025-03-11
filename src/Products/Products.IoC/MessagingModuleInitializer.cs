using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Common.Messaging;
using ProductManagement.Common.Messaging.Interfaces;
using ProductManagement.Common.Messaging.Services;

namespace Products.IoC;

public static class MessagingModuleInitializer
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MassTransitSettings>(configuration.GetSection("MasstransitSettings"));
        
        services.AddMassTransit(busConfig =>
        {
            busConfig.UsingRabbitMq((context, config) =>
            {
                var massTansitSettings = configuration.GetSection("MasstransitSettings").Get<MassTransitSettings>()
                    ?? throw new InvalidOperationException($"A chave {nameof(MassTransitSettings)} não foi encontrada ou não foi configurada corretamente.");
                
                config.Host(massTansitSettings.Host!, "/", x =>
                {
                    x.Username(massTansitSettings.User!);
                    x.Password(massTansitSettings.Password!);
                });

                config.ConfigureEndpoints(context);
            });
        });

        services.AddScoped<IQueueService, QueueService>();
        
        return services;
    }
}