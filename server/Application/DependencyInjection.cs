using Application._Common.Behaviors;
using Application._Common.Interfaces;
using Application._Common.Services;
using Application.Authentication.Commands;
using Application.Cities.Commands.CreateCity;
using Application.Counties.Commands.CreateCountry;
using Application.Countries.Commands.AddCityToCountry;
using Application.Countries.Commands.CreateCountry;
using Domain._Common.Interfaces;
using Domain.CityAggregate;
using Domain.UserAggregate;
using ErrorOr;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMapping();
        services.RegisterMediatr();
        services.AddCommandsValidation();
        services.AddServices();

        return services;
    }

    public static IServiceCollection RegisterMediatr(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            options.AddOpenBehavior(typeof(ValidationBehavior<,>));
            
            // options.AddBehavior<IPipelineBehavior<CreateCityCommand, ErrorOr<City>>, CreateCityCommandBehavior>();
        }); 

        return services;
    }

    public static IServiceCollection AddCommandsValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection));
        
        // services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();
        // services.AddScoped<IValidator<CreateCityCommand>, CreateCityCommandValidator>();
        // services.AddScoped<IValidator<CreateCountryCommand>, CreateCountryCommandValidator>();
        // services.AddScoped<IValidator<AddCityToCountryCommand>, AddCityToCountryCommandValidator>();

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