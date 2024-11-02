using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infraestructure.Persistance;

public static class MigrationManager
{
    public static void RunMigrations(IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<IdealCityDbContext>();
            context.Database.EnsureCreated();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while migrating the database : " + ex.Message);
        }
        finally
        {
            scope.Dispose();
        }
    }
}