using System.Text.Json.Serialization;
using Application._Common.Interfaces;
using Application._Common.Interfaces.Authentication;
using Application._Common.Services.Authentication;
using Contracts.Cities;
using Domain.Cities;
using Domain.Countries;
using Infraestructure.Common;
using Infraestructure.Common.Async;
using Infraestructure.Common.Async.Handlers;
using Infraestructure.Common.Async.Requests;
using Infraestructure.Persistance;
using Infraestructure.Persistance.Interceptors;
using Infraestructure.Persistance.Repositories;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(
        this IServiceCollection services,
        ConfigurationManager builderConfiguration)
    {
        services.AddExtensions();
        services.AddPersistance();
        services.AddRepositories();
        services.AddInterceptor();
        services.AddRabbitMqHandlers();
        services.AddRabbitMq(builderConfiguration);
        services.AddRabbitMqSubscriptions();
        services.AddAuthentication(builderConfiguration);

        return services;
    }

    public static IServiceCollection AddExtensions(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

        return services;
    }

    public static IServiceCollection AddPersistance(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        string connectionString = configuration.GetConnectionString("DefaultConnection")!;
        Console.WriteLine($"--> Connection string: {connectionString}");

        services.AddDbContext<IdealCityDbContext>(opt =>
            opt.UseNpgsql(connectionString, b => b.MigrationsAssembly("Infraestructure")));

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<ICountryRepository, CountryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    public static IServiceCollection AddInterceptor(this IServiceCollection services)
    {
        services.AddScoped<PublishDomainEventInterceptor>();
        services.AddScoped<SaveTimeStampsInterceptor>();
        return services;
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddAuthentication();
        return services;
    }

    public static IServiceCollection AddRabbitMq(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        // services.AddSingleton<IRabbitMqClient, RabbitMqClient>();

        services.AddSingleton<IAsyncBus, RabbitMqBus>(sp =>
            new RabbitMqBus(
                sp.GetRequiredService<IMediator>(),
                sp.GetRequiredService<IServiceScopeFactory>(),
                configuration["RabbitMQ:HostName"]!,
                sp.GetRequiredService<IMapper>()
            )
        );
        return services;
    }
    
    public static IServiceCollection AddRabbitMqHandlers(this IServiceCollection services)
    {
        services.AddSingleton<IHandler<CreateCityQueueRequest>, CreateCityQueueHandler>();
        return services;
    }

    // Register queues being consumed
    public static IServiceCollection AddRabbitMqSubscriptions(this IServiceCollection services)
    {
        var sp = services.BuildServiceProvider();
        var amqpBus = sp.GetRequiredService<IAsyncBus>();
        amqpBus.Subscribe("scrapper");

        return services;
    }
}