using Auth.Jwt.Interfaces;
using Auth.Jwt.Models;
using Auth.Jwt.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Jwt.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        
        services.AddScoped<IJwtService, JwtService>();

        return services;
    }
}