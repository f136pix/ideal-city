using Domain.Cities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infraestructure.Persistance;

public class IdealCityDbContext : DbContext
{
    public IdealCityDbContext(DbContextOptions<IdealCityDbContext> options) : base(options)
    {
    }

    public DbSet<City> Cities { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.
            ApplyConfigurationsFromAssembly(typeof(IdealCityDbContext).Assembly);

        // modelBuilder.Model.GetEntityTypes()
        //     .SelectMany(e => e.GetProperties())
        //     .Where(p => p.IsPrimaryKey())
        //     .ToList()
        //     .ForEach(p => p.ValueGenerated = ValueGenerated.Never);
        //
        base.OnModelCreating(modelBuilder);
    }
}