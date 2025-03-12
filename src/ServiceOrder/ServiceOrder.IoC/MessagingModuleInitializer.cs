using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Common.Messaging;
using ServiceOrder.Application.Consumers;
using ServiceOrder.Application.Consumers.Product;

namespace ServiceOrder.IoC;

public static class MessagingModuleInitializer
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MassTransitSettings>(configuration.GetSection("MasstransitSettings"));
        
        services.AddMassTransit(busConfig =>
        {
            busConfig.AddConsumer<ProductCreatedConsumer>();
            
            busConfig.UsingRabbitMq((context, config) =>
            {
                var massTansitSettings = configuration.GetSection("MasstransitSettings").Get<MassTransitSettings>()
                                         ?? throw new InvalidOperationException($"A chave {nameof(MassTransitSettings)} não foi encontrada ou não foi configurada corretamente.");
                
                config.Host(massTansitSettings.Host!, "/", x =>
                {
                    x.Username(massTansitSettings.User!);
                    x.Password(massTansitSettings.Password!);
                });

                config.ReceiveEndpoint("product_queue", e =>
                {
                    e.ConfigureConsumer<ProductCreatedConsumer>(context);
                });
            });
        });
        
        return services;
    }
}