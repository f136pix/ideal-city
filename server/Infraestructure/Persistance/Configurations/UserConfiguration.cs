using Domain.City.ValueObjects;
using Domain.CityAggregate;
using Domain.Common;
using Domain.User;
using Domain.User.ValueObject;
using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistance.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>, IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ConfigureUsersTable(builder);
        ConfigurePostsTable(builder);
        // ConfigurePostsIdsTable(builder);
        // ConfigureUserFavouritesRelation(builder);
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
        // builder.HasOne(u => u.Subscription)
        //     .WithMany(s => s.Users)
        //     .HasPrincipalKey(s => s.Id)
        //     .HasForeignKey(u => u.SubscriptionId);


        // IGNORE POSTS IDS, SINCE ARE USED ONLY FOR NAVIGATION
        builder.Ignore(u => u.PostIds);

        // builder.HasMany(u => u.Posts)
        // .WithOne(p => p.User)
        // .HasForeignKey(p => p.UserId);
    }

    private void ConfigurePostsTable(EntityTypeBuilder<User> builder)
    {
        // ONE USER HAS MANY POSTS
        builder.OwnsMany(u => u.Posts, pb =>
        {
            pb.ToTable("Posts");
            pb.HasKey(p => p.Id);
            pb.WithOwner(p => p.User)
                .HasForeignKey(p => p.UserId);
            pb.Property(p => p.Id)
                .HasColumnName("Id")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => PostId.Create(value));
            pb.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(50);
            pb.Property(p => p.Content)
                .IsRequired()
                .HasMaxLength(255);
            pb.Property(p => p.CreatedAt)
                .IsRequired();
            pb.Property(p => p.UpdatedAt)
                .IsRequired();
            pb.Property(p => p.UserId)
                .IsRequired()
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value));
        });
    }

    private void ConfigurePostsIdsTable(EntityTypeBuilder<User> builder)
    {
        // ONE USER HAS MANY POSTS IDS
        builder.OwnsMany(user => user.PostIds, pub =>
        {
            pub.ToTable("UserPostIds");

            pub.HasKey("Id"); // ID KEY

            pub.WithOwner().HasForeignKey("UserId"); // POST ID FOREIGN KEY -> COMES FROM BASE BUILDER

            pub.Property(upi => upi.Value) 
                .HasColumnName("PostId")
                .ValueGeneratedNever();
        });

        builder.Metadata.FindNavigation(nameof(User.PostIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        ConfigureSubscriptionsTable(builder);
        // ConfigureSubscriptionsIdsTable(builder);
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

        builder.Property(s => s.IsActive)
            .HasColumnType("bool");

        builder.Property(s => s.ExpirationDate);


        builder.HasMany(s => s.Users)
            .WithOne(u => u.Subscription)
            .HasPrincipalKey(s => s.Id)
            .HasForeignKey(u => u.SubscriptionId);
    }
}