using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using RentalCar.Service.Application.Handlers;
using RentalCar.Service.Application.Validators;

namespace RentalCar.Service.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddFluentValidation()
            .AddHandlers()
            .AddBackgroundServices();
        return services;
    }


    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<CreateServiceValidator>();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CreateServiceHandler>());

        return services;
    }
    
    private static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        //services.AddHostedService<ServiceBackgroundService>();
        
        return services;
    }

}

