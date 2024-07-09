using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CountryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CostIndex = table.Column<string>(type: "text", nullable: true),
                    PublicTransportationIndex = table.Column<string>(type: "text", nullable: true),
                    Gasoline = table.Column<string>(type: "text", nullable: true),
                    AverageMonthlyNetSalary = table.Column<string>(type: "text", nullable: true),
                    AverageTemperature = table.Column<string>(type: "text", nullable: true),
                    AverageRatingValue = table.Column<double>(type: "double precision", nullable: true),
                    AverageRatingTotalRatings = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CityReviewIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReviewId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityReviewIds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CityReviewIds_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CityReviews",
                columns: table => new
                {
                    CityReviewId = table.Column<Guid>(type: "uuid", nullable: false),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Review = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityReviews", x => new { x.CityReviewId, x.CityId });
                    table.ForeignKey(
                        name: "FK_CityReviews_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityReviewIds_CityId",
                table: "CityReviewIds",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CityReviews_CityId",
                table: "CityReviews",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityReviewIds");

            migrationBuilder.DropTable(
                name: "CityReviews");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
