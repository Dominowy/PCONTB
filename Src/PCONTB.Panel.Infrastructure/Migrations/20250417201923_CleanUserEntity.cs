using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCONTB.Panel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CleanUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Category_CategoryId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Country_CountryId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Browser",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "Device",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "IpAddress",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "OperatingSystem",
                table: "Session");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Category_CategoryId",
                table: "Project",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Country_CountryId",
                table: "Project",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Category_CategoryId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Country_CountryId",
                table: "Project");

            migrationBuilder.AddColumn<string>(
                name: "Browser",
                table: "Session",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Device",
                table: "Session",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IpAddress",
                table: "Session",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Session",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OperatingSystem",
                table: "Session",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Category_CategoryId",
                table: "Project",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Country_CountryId",
                table: "Project",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
