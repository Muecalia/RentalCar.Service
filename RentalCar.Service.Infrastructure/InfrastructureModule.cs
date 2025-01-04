using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using RentalCar.Service.Core.Repositories;
using RentalCar.Service.Core.Service;
using RentalCar.Service.Infrastructure.MessageBus;
using RentalCar.Service.Infrastructure.Prometheus;
using RentalCar.Service.Infrastructure.Repositories;
using RentalCar.Service.Infrastructure.Service;

namespace RentalCar.Service.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) 
    {
        services
            .AddServices()
            .AddOpenTelemetryConfig()
            ;
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services) 
    {
        services.AddSingleton<ILoggerService, LoggerService>();
        services.AddSingleton<IRabbitMqService, RabbitMqService>();
        services.AddSingleton<IPrometheusService, PrometheusService>();
        services.AddSingleton<IModelService, ModelService>();

        //services.AddSingleton<IRedisRepository, RedisRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();

        return services;
    }
    
    private static IServiceCollection AddOpenTelemetryConfig(this IServiceCollection services)
    {
        const string serviceName = "RentalCar Service";
        const string serviceVersion = "v1";
        
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithTracing(tracing => tracing
                .SetResourceBuilder(ResourceBuilder.CreateDefault()
                    .AddService(serviceName: serviceName, serviceVersion:serviceVersion))
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter()
                .AddConsoleExporter())
            .WithMetrics(metrics => metrics
                .AddConsoleExporter()
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddPrometheusExporter()
            );

        return services;
    }

}