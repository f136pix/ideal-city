using Application._Common.Interfaces;
using Application._Common.Services;
using Application.Authentication.Commands;
using Application.Cities.Commands.CreateCity;
using Application.Counties.Commands.CreateCountry;
using Application.Countries.Commands.AddCityToCountry;
using Application.Countries.Commands.CreateCountry;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMapping();
        services.AddMediatR(options => { options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly); });
        services.AddCommandsValidation();
        services.AddServices();

        return services;
    }

    public static IServiceCollection AddCommandsValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();
        services.AddScoped<IValidator<CreateCityCommand>, CreateCityCommandValidator>();
        services.AddScoped<IValidator<CreateCountryCommand>, CreateCountryCommandValidator>();
        services.AddScoped<IValidator<AddCityToCountryCommand>, AddCityToCountryCommandValidator>();

        // services.AddFluentValidationAutoValidation();
        return services;
    }

    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        services.AddMapster();
        TypeAdapterConfig.GlobalSettings.Scan(typeof(DependencyInjection).Assembly);
        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IApplicationService, ApplicationCommonService>();
        return services;
    }
}