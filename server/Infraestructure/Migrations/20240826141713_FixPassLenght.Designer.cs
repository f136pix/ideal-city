﻿// <auto-generated />
using System;
using Infraestructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infraestructure.Migrations
{
    [DbContext(typeof(IdealCityDbContext))]
    [Migration("20240826141713_FixPassLenght")]
    partial class FixPassLenght
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.CityAggregate.City", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities", (string)null);
                });

            modelBuilder.Entity("Domain.CountryAggregate.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Countries", (string)null);
                });

            modelBuilder.Entity("Domain.User.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Posts", (string)null);
                });

            modelBuilder.Entity("Domain.User.ValueObject.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("ExpirationDate");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bool");

                    b.Property<string>("SubscriptionType")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserIds")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Subscriptions", (string)null);
                });

            modelBuilder.Entity("Domain.UserAggregate.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<string>("Bio")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid?>("CityId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfilePicture")
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<Guid>("SubscriptionId")
                        .HasColumnType("uuid")
                        .HasColumnName("SubscriptionId");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("SubscriptionId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Domain.CityAggregate.City", b =>
                {
                    b.HasOne("Domain.CountryAggregate.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Domain.Cities.Weather", "Weather", b1 =>
                        {
                            b1.Property<Guid>("CityId")
                                .HasColumnType("uuid");

                            b1.Property<string>("AverageTemperature")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("AverageTemperature");

                            b1.HasKey("CityId");

                            b1.ToTable("Cities");

                            b1.WithOwner()
                                .HasForeignKey("CityId");
                        });

                    b.OwnsMany("Domain.City.ValueObjects.CityReview", "Reviews", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid")
                                .HasColumnName("Id");

                            b1.Property<Guid>("CityId")
                                .HasColumnType("uuid")
                                .HasColumnName("CityId");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<int>("Rating")
                                .HasColumnType("int");

                            b1.Property<string>("Review")
                                .IsRequired()
                                .HasMaxLength(1000)
                                .HasColumnType("character varying(1000)");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("timestamp with time zone");

                            b1.HasKey("Id");

                            b1.HasIndex("CityId");

                            b1.ToTable("CityReviews", (string)null);

                            b1.WithOwner("City")
                                .HasForeignKey("CityId");

                            b1.Navigation("City");
                        });

                    b.OwnsOne("Domain.City.ValueObjects.Indicator", "Indicators", b1 =>
                        {
                            b1.Property<Guid>("CityId")
                                .HasColumnType("uuid");

                            b1.Property<string>("AverageMonthlyNetSalary")
                                .HasColumnType("text")
                                .HasColumnName("AverageMonthlyNetSalary");

                            b1.Property<string>("CostIndex")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("CostIndex");

                            b1.Property<string>("Gasoline")
                                .HasColumnType("text")
                                .HasColumnName("Gasoline");

                            b1.Property<string>("PublicTransportationIndex")
                                .HasColumnType("text")
                                .HasColumnName("PublicTransportationIndex");

                            b1.HasKey("CityId");

                            b1.ToTable("Cities");

                            b1.WithOwner()
                                .HasForeignKey("CityId");
                        });

                    b.OwnsOne("Domain.Common.ValueObjects.AverageRating", "AverageRating", b1 =>
                        {
                            b1.Property<Guid>("CityId")
                                .HasColumnType("uuid");

                            b1.Property<int>("TotalRatings")
                                .HasColumnType("integer")
                                .HasColumnName("AverageRatingTotalRatings");

                            b1.Property<double>("Value")
                                .HasColumnType("double precision")
                                .HasColumnName("AverageRatingValue");

                            b1.HasKey("CityId");

                            b1.ToTable("Cities");

                            b1.WithOwner()
                                .HasForeignKey("CityId");
                        });

                    b.Navigation("AverageRating");

                    b.Navigation("Country");

                    b.Navigation("Indicators");

                    b.Navigation("Reviews");

                    b.Navigation("Weather");
                });

            modelBuilder.Entity("Domain.User.Entities.Post", b =>
                {
                    b.HasOne("Domain.UserAggregate.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.UserAggregate.User", b =>
                {
                    b.HasOne("Domain.CityAggregate.City", "City")
                        .WithMany("Users")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Domain.User.ValueObject.Subscription", "Subscription")
                        .WithMany("Users")
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("Domain.CityAggregate.City", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.CountryAggregate.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("Domain.User.ValueObject.Subscription", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.UserAggregate.User", b =>
                {
                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
