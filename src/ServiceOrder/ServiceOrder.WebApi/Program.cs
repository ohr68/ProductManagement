using ProductManagement.Common.Filters;
using ProductManagement.Common.HealthChecks;
using ProductManagement.Common.Logging;
using Serilog;
using ServiceOrder.IoC;
using ServiceOrder.ORM.Context;
using ServiceOrder.WebApi.Constants;
using ServiceOrder.WebApi.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Log.Information("Starting web application");

            var builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();

            builder.Services.AddProblemDetails(options =>
                options.CustomizeProblemDetails = ctx =>
                {
                    ctx.ProblemDetails.Extensions.Add("trace-id", ctx.HttpContext.TraceIdentifier);
                    ctx.ProblemDetails.Extensions.Add("instance",
                        $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}");
                });

            builder.Services.AddPresentationLayer(builder.Configuration);
            builder.Services.ConfigureServices(builder.Configuration, builder.Environment.IsDevelopment());

            builder.Services.AddControllers(options => { options.Filters.Add<GlobalExceptionFilter>(); });

            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();
            
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Service Orders Web API V1");
                });
            }

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseBasicHealthChecks(); 
            app.UseCors(Configuration.AllowProductManagementClient);
            app.MapControllers();

            // When the app runs, it first creates the Database.
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ServiceOrderDbContext>();
            context.Database.EnsureCreated();

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
            Console.WriteLine($"Critical error: {ex.Message}");
            Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
            Console.WriteLine(ex.StackTrace);
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}