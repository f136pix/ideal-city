using Application._Common.Interfaces;
using Domain.Cities;
using Domain.Countries;
using Infraestructure.Common;
using Infraestructure.Persistance;
using Infraestructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services)
    {
        services.AddPersistance();

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
            
        services.AddScoped<ICommonRepository<City>, CitiesRepository>();
        services.AddScoped<ICommonRepository<Country>, CountriesRepository>();

        return services;
    }
    
}