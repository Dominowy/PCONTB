using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCONTB.Panel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ManageCommunityPermission",
                table: "Collaborator",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ManageFulfillmentPermission",
                table: "Collaborator",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ManageProjectPermission",
                table: "Collaborator",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ManageCommunityPermission",
                table: "Collaborator");

            migrationBuilder.DropColumn(
                name: "ManageFulfillmentPermission",
                table: "Collaborator");

            migrationBuilder.DropColumn(
                name: "ManageProjectPermission",
                table: "Collaborator");
        }
    }
}
