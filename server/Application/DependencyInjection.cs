using Application.Cities.Commands.CreateCity;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMapping();
        services.AddMediatR(options => { options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly); });
        services.AddCommandsValidation();

        return services;
    }

    public static IServiceCollection AddCommandsValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateCityCommand>, CreateCityCommandValidator>();
        // services.AddFluentValidationAutoValidation();
        return services;
    }

    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        services.AddMapster();
        TypeAdapterConfig.GlobalSettings.Scan(typeof(DependencyInjection).Assembly);
        return services;
    }
}