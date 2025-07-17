using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCONTB.Panel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectCampaign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CampaingId",
                table: "Project",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProjectCampaign",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCampaign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectCampaign_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCampaignContent",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    CampaignId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Data = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCampaignContent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectCampaignContent_ProjectCampaign_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "ProjectCampaign",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_CampaingId",
                table: "Project",
                column: "CampaingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCampaign_ProjectId",
                table: "ProjectCampaign",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCampaignContent_CampaignId",
                table: "ProjectCampaignContent",
                column: "CampaignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ProjectCampaign_CampaingId",
                table: "Project",
                column: "CampaingId",
                principalTable: "ProjectCampaign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_ProjectCampaign_CampaingId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "ProjectCampaignContent");

            migrationBuilder.DropTable(
                name: "ProjectCampaign");

            migrationBuilder.DropIndex(
                name: "IX_Project_CampaingId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CampaingId",
                table: "Project");
        }
    }
}
