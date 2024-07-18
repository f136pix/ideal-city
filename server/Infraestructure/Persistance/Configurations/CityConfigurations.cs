using Domain.Cities;
using Domain.City.ValueObjects;
using Domain.CityAggregate;
using Domain.Countries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistance.Configurations;

public class CityConfigurations : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        ConfigureCitiesTable(builder);
        ConfigureCityReviewsTable(builder);
        // ConfigureCityReviewsIdsTable(builder);
        ConfigureCityFavouritesRelation(builder);
    }


    private void ConfigureCitiesTable(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");

        // CITY ID
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value, // when sending to the database sends the id value
                value => CityId.Create(value)); // when recieving from the database creates a new CityId object

        // CITY NAME
        builder.Property(c => c.Name)
            .HasMaxLength(100);

        // COUNTRY ID
        builder.Property(c => c.CountryId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CountryId.Create(value));

        // ONE CITY BELONGS TO ONE COUNTRY
        builder.HasOne(c => c.Country)
            .WithMany(c => c.Cities)
            .HasForeignKey(c => c.CountryId)
            .OnDelete(DeleteBehavior.Cascade);

        // INDICATORS - EACH ONE IS A COLUMN IN THE CITIES TABLE
        builder.OwnsOne(c => c.Indicators, ib =>
        {
            ib.Property(ind => ind.CostIndex).HasColumnName("CostIndex");
            ib.Property(ind => ind.PublicTransportationIndex).HasColumnName("PublicTransportationIndex");
            ib.Property(ind => ind.Gasoline).HasColumnName("Gasoline");
            ib.Property(ind => ind.AverageMonthlyNetSalary).HasColumnName("AverageMonthlyNetSalary");
        });

        // WEATHERS - EACH ONE IS A COLUMN IN THE CITIES TABLE
        builder.OwnsOne(c => c.Weather,
            wb => { wb.Property(w => w.AverageTemperature).HasColumnName("AverageTemperature"); });

        // CITY AVRG RATINGS
        builder.OwnsOne(c => c.AverageRating, ab =>
        {
            ab.Property(ar => ar.Value)
                .HasColumnName("AverageRatingValue");
            ab.Property(ar => ar.TotalRatings)
                .HasColumnName("AverageRatingTotalRatings");
        });

        builder.Ignore(c => c.ReviewsIds);
        builder.Ignore(c => c.UsersIds);

        // ONE CITY HAS MANY USERS
        builder.HasMany(c => c.Users)
            .WithOne(c => c.City)
            .HasForeignKey(u => u.CityId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata.FindNavigation(nameof(City.Users))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private void ConfigureCityReviewsTable(EntityTypeBuilder<City> builder)
    {
        builder.OwnsMany(c => c.Reviews, rb =>
        {
            rb.ToTable("CityReviews");

            // FOREIGN KEY TO CITY TABLE
            rb.WithOwner(r => r.City)
                .HasForeignKey(r => r.CityId)
                .HasPrincipalKey(c => c.Id);
            
            
            // INDEXES
            rb.HasKey(nameof(CityReview.Id));

            rb.Property(r => r.Id)
                .HasColumnName("Id")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => CityReviewId.Create(value));

            rb.Property(r => r.CityId)
                .HasColumnName("CityId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => CityId.Create(value))
                .IsRequired();

            rb.Property(r => r.Review)
                .HasMaxLength(1000);

            rb.Property(r => r.Rating)
                .HasColumnType("int");
        });

        builder.Metadata.FindNavigation(nameof(City.Reviews))!
            .SetPropertyAccessMode(PropertyAccessMode.Field); // MODE THAT USES THE _reviews AS A BACKING FIELD
    }

    private void ConfigureCityReviewsIdsTable(EntityTypeBuilder<City> builder)
    {
        builder.OwnsMany(c => c.ReviewsIds, cib =>
        {
            cib.ToTable("CityReviewIds");

            cib.HasKey("Id"); // ID KEY

            cib.WithOwner().HasForeignKey("CityId"); // CITY ID FOREIGN KEY -> COMES FROM BASE BUILDER

            cib.Property(cri => cri.Value) // CITY REVIEW ID FOREIGN KEY 
                .HasColumnName("ReviewId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(City.ReviewsIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field); // MODE THAT USES THE _reviewsIds AS A BACKING FIELD
    }

    private void ConfigureCityFavouritesRelation(EntityTypeBuilder<City> builder)
    {
    }
}