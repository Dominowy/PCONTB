using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCONTB.Panel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEnabled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Session",
                newName: "Enabled");

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Subcategory",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Project",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Country",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Category",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Subcategory");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "Enabled",
                table: "Session",
                newName: "IsActive");
        }
    }
}
