using Domain.City.ValueObjects;
using Domain.CityAggregate;
using Domain.Common;
using Domain.User;
using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistance.Configurations;

// public class UserConfiguration : IEntityTypeConfiguration<User>
// {
//     public void Configure(EntityTypeBuilder<User> builder)
//     {
//         ConfigureUsersTable(builder);
//         ConfigureUserFavouritesRelation(builder);
//     }
//
//     private void ConfigureUsersTable(EntityTypeBuilder<User> builder)
//     {
//         builder.ToTable("Users");
//
//         // USER ID
//         builder.HasKey(u => u.Id);
//         builder.Property(u => u.Id)
//             .HasColumnName("Id")
//             .ValueGeneratedNever()
//             .HasConversion(
//                 id => id.Value,
//                 value => UserId.Create(value));
//
//         // USER NAME
//         builder.Property(u => u.Name)
//             .HasMaxLength(100);
//
//         // USER EMAIL
//         builder.Property(u => u.Email)
//             .HasMaxLength(100);
//
//         // USER PASSWORD
//         builder.Property(u => u.Password)
//             .HasMaxLength(100);
//
//         // USER PROFILE PICTURE
//         builder.Property(u => u.ProfilePicture)
//             .HasMaxLength(300);
//
//         // USER BIO
//         builder.Property(u => u.Bio)
//             .HasMaxLength(1000);
//
//         // CITY ID
//         builder.Property(u => u.CityId)
//             .HasColumnName("CityId")
//             .ValueGeneratedNever()
//             .HasConversion(
//                 id => id.Value,
//                 value => CityId.Create(value));
//
//         // ONE USER BELONGS TO ONE CITY
//         builder.HasOne(u => u.City)
//             .WithMany(c => c.Users)
//             .HasForeignKey(u => u.CityId)
//             .IsRequired(false)
//             .OnDelete(DeleteBehavior.SetNull);
//
//         // USER SUBSCRIPTION
//         builder.OwnsOne(u => u.Subscription, sb =>
//         {
//             sb.Property(s => s.SubscriptionType)
//                 .HasColumnName("SubscriptionType")
//                 .HasConversion(
//                     v => v == SubscriptionType.Pro ? 1 : 0,
//                     v => v == 1 ? SubscriptionType.Pro : SubscriptionType.Basic);
//
//             sb.Property(s => s.ExpirationDate)
//                 .HasColumnName("ExpirationDate");
//         });
//     }
//
//     private void ConfigureUserFavouritesRelation(EntityTypeBuilder<User> builder)
//     {
//     }
// }