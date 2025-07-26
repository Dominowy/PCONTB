using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCONTB.Panel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCommunityFavoriteAndRebuildDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCampaign_Project_ProjectId",
                table: "ProjectCampaign");

            migrationBuilder.DropIndex(
                name: "IX_ProjectCampaign_ProjectId",
                table: "ProjectCampaign");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProjectCampaign");

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "ProjectCampaign",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CommunityId",
                table: "Project",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Project",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Modified",
                table: "Project",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Project",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProjectCommunity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCommunity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProjectFavorite",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProjectFavorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProjectFavorite_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProjectFavorite_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommunityMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    WalletAddress = table.Column<string>(type: "text", nullable: false),
                    ProjectCommunityId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunityMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunityMessage_CommunityMessage_ParentId",
                        column: x => x.ParentId,
                        principalTable: "CommunityMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommunityMessage_ProjectCommunity_ProjectCommunityId",
                        column: x => x.ProjectCommunityId,
                        principalTable: "ProjectCommunity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommunityMessage_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_CommunityId",
                table: "Project",
                column: "CommunityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommunityMessage_ParentId",
                table: "CommunityMessage",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityMessage_ProjectCommunityId",
                table: "CommunityMessage",
                column: "ProjectCommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunityMessage_UserId",
                table: "CommunityMessage",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectFavorite_ProjectId",
                table: "UserProjectFavorite",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectFavorite_UserId",
                table: "UserProjectFavorite",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ProjectCommunity_CommunityId",
                table: "Project",
                column: "CommunityId",
                principalTable: "ProjectCommunity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_ProjectCommunity_CommunityId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "CommunityMessage");

            migrationBuilder.DropTable(
                name: "UserProjectFavorite");

            migrationBuilder.DropTable(
                name: "ProjectCommunity");

            migrationBuilder.DropIndex(
                name: "IX_Project_CommunityId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "ProjectCampaign");

            migrationBuilder.DropColumn(
                name: "CommunityId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Modified",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Project");

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "ProjectCampaign",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCampaign_ProjectId",
                table: "ProjectCampaign",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCampaign_Project_ProjectId",
                table: "ProjectCampaign",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
