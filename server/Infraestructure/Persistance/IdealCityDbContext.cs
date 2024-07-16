using Domain.Cities;
using Domain.City.ValueObjects;
using Domain.CityAggregate;
using Domain.Common;
using Domain.CountryAggregate;
using Domain.User;
using Domain.UserAggregate;
using Infraestructure.Persistance.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Infraestructure.Persistance;

public class IdealCityDbContext : DbContext
{
    private readonly PublishDomainEventInterceptor _publishDomainEventInterceptor;

    public IdealCityDbContext(DbContextOptions<IdealCityDbContext> options,
        PublishDomainEventInterceptor publishDomainEventInterceptor) : base(options)
    {
        _publishDomainEventInterceptor = publishDomainEventInterceptor;
    }


    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<CityReview> CityReviews { get; set; } = null!;
    // public DbSet<User> Users { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>() // ignore any list of domain events
            .ApplyConfigurationsFromAssembly(typeof(IdealCityDbContext).Assembly);

        // modelBuilder.Model.GetEntityTypes()
        //     .SelectMany(e => e.GetProperties())
        //     .Where(p => p.IsPrimaryKey())
        //     .ToList()
        //     .ForEach(p => p.ValueGenerated = ValueGenerated.Never);
        //
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}