using Domain.City.ValueObjects;
using Domain.CityAggregate;
using Domain.Common;
using Domain.User;
using Domain.User.Entities;
using Domain.User.ValueObject;
using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistance.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>, IEntityTypeConfiguration<Subscription>,
    IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
    }

    private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(c => c.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value, // when sending to the database sends the id value
                value => UserId.Create(value)); // when recieving from the database creates a new UserID object

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.ProfilePicture)
            .HasMaxLength(255);

        builder.Property(u => u.Bio)
            .HasMaxLength(255);

        // CITY ID CONVERSION
        builder.Property(u => u.CityId)
            .HasConversion(
                id => id.Value,
                value => CityId.Create(value));

        // ONE USER BELONGS TO ONE CITY
        builder.HasOne(u => u.City)
            .WithMany(c => c.Users)
            .HasPrincipalKey(c => c.Id) // our foreign key points to city id
            .HasForeignKey(u => u.CityId);

        // SUBSCRIPTION ID CONVERSION
        builder.Property(u => u.SubscriptionId)
            .HasColumnName("SubscriptionId")
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => SubscriptionId.Create(value));

        // BEING DEFINED BELOW IN SUBSCRIPTIONS SESSION
        // ONE USER BELONGS TO ONE SUBSCRIPTION
        builder.HasOne(u => u.Subscription)
            .WithMany(s => s.Users)
            .HasPrincipalKey(s => s.Id)
            .HasForeignKey(u => u.SubscriptionId);

        builder.HasMany(u => u.Posts)
            .WithOne(p => p.User)
            .HasForeignKey(c => c.UserId)
            .HasPrincipalKey(u => u.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Metadata.FindNavigation(nameof(User.Posts))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        // IGNORE POSTS IDS, SINCE ARE USED ONLY FOR NAVIGATION
        builder.Ignore(u => u.PostIds);
    }

    public void Configure(EntityTypeBuilder<Post> builder)
    {
        ConfigurePostsTable(builder);
    }

    private void ConfigurePostsTable(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasColumnName("Id")
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => PostId.Create(value));
        // builder.Property(p => p.Title)
        //     .IsRequired()
        //     .HasMaxLength(50);
        // builder.Property(p => p.Content)
        //     .IsRequired()
        //     .HasMaxLength(255);
        builder.Property(p => p.UserId)
            .IsRequired()
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));
    }

    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        ConfigureSubscriptionsTable(builder);
    }

    private void ConfigureSubscriptionsTable(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("Subscriptions");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .HasConversion(
                id => id.Value,
                value => SubscriptionId.Create(value))
            .ValueGeneratedNever();

        builder.Property(s => s.SubscriptionType)
            .HasColumnType("varchar(50)")
            .HasConversion(s => s.Name,
                v => SubscriptionType.FromName(v, false));

        builder.Property(s => s.IsActive)
            .HasColumnType("bool");

        builder.Property(s => s.ExpirationDate)
            .HasColumnName("ExpirationDate");

        builder.Metadata.FindNavigation(nameof(Subscription.Users))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(s => s.Users)
            .WithOne(u => u.Subscription)
            .HasPrincipalKey(s => s.Id)
            .HasForeignKey(u => u.SubscriptionId);

        builder.Metadata.FindNavigation(nameof(Subscription.UserIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
        
          builder.Property(s => s.UserIds)
                    .HasListOfIdsConverter();

        // builder.Property<List<UserId>>("_userIds")
        //     .HasColumnName("UserIds")
        //     .HasListOfIdsConverter();
    }
}