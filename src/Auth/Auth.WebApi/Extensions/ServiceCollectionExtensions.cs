using System.Reflection;
using System.Text;
using Auth.WebApi.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Auth.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddSwagger(configuration)
            .ConfigureCors(configuration);
        
        return services;
    }
    
    private static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Auth Web Web API",
                Description = ""
            });

            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
        });
        
        return services;
    }

    private static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(Configuration.AllowProductManagementClient, policy =>
            {
                policy.WithOrigins(configuration.GetSection("AllowedClients:ProductManagement").Value!)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        
        return services;
    }
}