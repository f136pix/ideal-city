using Domain.Countries;
using Domain.CountryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistance.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        ConfigureCountriesTable(builder);
        ConfigureCitiesRelation(builder);
    }

    private void ConfigureCountriesTable(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");

        // COUNTRY ID
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CountryId.Create(value));

        // COUNTRY NAME
        builder.Property(c => c.Name)
            .HasMaxLength(100);

        // CITIES IDS - IGNORE SINCE ARE USED ONLY FOR NAVIGATION
        builder.Ignore(c => c.CityIds);
    }

    private void ConfigureCitiesRelation(EntityTypeBuilder<Country> builder)
    {
        // CITIES AGGREGATES
        builder.HasMany(c => c.Cities)
            .WithOne(c => c.Country)
            .HasForeignKey(c => c.CountryId) // CITIES table has foreign key to countryId
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata.FindNavigation(nameof(Country.Cities))!
            .SetPropertyAccessMode(PropertyAccessMode.Field); // MODE THAT USES THE _cities AS A BACKING FIELD
    }
   
}