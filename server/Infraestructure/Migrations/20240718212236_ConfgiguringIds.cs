using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfgiguringIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityReviewIds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CityReviews",
                table: "CityReviews");

            migrationBuilder.RenameColumn(
                name: "CityReviewId",
                table: "CityReviews",
                newName: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "CityId1",
                table: "CityReviews",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CityReviews",
                table: "CityReviews",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_CityReviews_CityId1",
                table: "CityReviews",
                column: "CityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CityReviews_Cities_CityId1",
                table: "CityReviews",
                column: "CityId1",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityReviews_Cities_CityId1",
                table: "CityReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CityReviews",
                table: "CityReviews");

            migrationBuilder.DropIndex(
                name: "IX_CityReviews_CityId1",
                table: "CityReviews");

            migrationBuilder.DropColumn(
                name: "CityId1",
                table: "CityReviews");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CityReviews",
                newName: "CityReviewId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CityReviews",
                table: "CityReviews",
                columns: new[] { "CityReviewId", "CityId" });

            migrationBuilder.CreateTable(
                name: "CityReviewIds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CityId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReviewId = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_CityReviewIds_CityId",
                table: "CityReviewIds",
                column: "CityId");
        }
    }
}
