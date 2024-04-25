using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.BookStore.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<Guid>(
                name: "CountryId",
                table: "AppAuthors",
                type: "uniqueidentifier",
                nullable: true,
                defaultValue: null);

            migrationBuilder.CreateTable(
                name: "AppCountries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCountries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppAuthors_CountryId",
                table: "AppAuthors",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppCountries_Name",
                table: "AppCountries",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_AppAuthors_AppCountries_CountryId",
                table: "AppAuthors",
                column: "CountryId",
                principalTable: "AppCountries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppAuthors_AppCountries_CountryId",
                table: "AppAuthors");

            migrationBuilder.DropTable(
                name: "AppCountries");

            migrationBuilder.DropIndex(
                name: "IX_AppAuthors_CountryId",
                table: "AppAuthors");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "AppAuthors");
        }
    }
}
