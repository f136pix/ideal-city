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
        ConfigureCitiesIdsRelation(builder);
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
    }

    private void ConfigureCitiesRelation(EntityTypeBuilder<Country> builder)
    {
        // CITIES AGGREGATES
        builder.HasMany(c => c.Cities)
            .WithOne(c => c.Country)
            .HasForeignKey("CountryId") // CITIES table has foreign key to countryId
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata.FindNavigation(nameof(Country.Cities))!
            .SetPropertyAccessMode(PropertyAccessMode.Field); // MODE THAT USES THE _cities AS A BACKING FIELD
    }

    private void ConfigureCitiesIdsRelation(EntityTypeBuilder<Country> builder)
    {
        builder.OwnsMany(c => c.CityIds, cib => // DEFINING CITY ID AS AN ENTITY TYPE - HAS A TABLE 
        {
            cib.ToTable("CountryCitiesIds"); // TABLE THAT RELATES COUNTRY TO CITIES

            cib.WithOwner().HasForeignKey("CountryId"); // COUNTRY ID FOREIGN KEY
            cib.HasKey("Id");
            cib.Property(ci => ci.Value)
                .HasColumnName("CityId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(Country.CityIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field); // MODE THAT USES THE _cityIds AS A BACKING FIELD
    }
}