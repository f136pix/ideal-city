using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityReviews_Cities_CityId1",
                table: "CityReviews");

            migrationBuilder.DropIndex(
                name: "IX_CityReviews_CityId1",
                table: "CityReviews");

            migrationBuilder.DropColumn(
                name: "CityId1",
                table: "CityReviews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CityId1",
                table: "CityReviews",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
    }
}
