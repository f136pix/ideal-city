using Domain.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistance.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        ConfigureCountriesTable(builder);
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
        
        // ONE COUNTRY HAS MANY CITIES
        builder.OwnsMany(c => c.CityIds);
        builder.HasMany(c => c.Cities)
            .WithOne(c => c.Country)
            .HasForeignKey("CountryId") // CITIES table has foreign key to countryId
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Metadata.FindNavigation(nameof(Country.Cities))!
            .SetPropertyAccessMode(PropertyAccessMode.Field); // MODE THAT USES THE _cities AS A BACKING FIELD
        
        builder.Metadata.FindNavigation(nameof(Country.CityIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}