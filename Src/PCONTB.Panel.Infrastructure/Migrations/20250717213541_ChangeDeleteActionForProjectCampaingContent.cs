using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCONTB.Panel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeleteActionForProjectCampaingContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCampaignContent_ProjectCampaign_CampaignId",
                table: "ProjectCampaignContent");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCampaignContent_ProjectCampaign_CampaignId",
                table: "ProjectCampaignContent",
                column: "CampaignId",
                principalTable: "ProjectCampaign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectCampaignContent_ProjectCampaign_CampaignId",
                table: "ProjectCampaignContent");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectCampaignContent_ProjectCampaign_CampaignId",
                table: "ProjectCampaignContent",
                column: "CampaignId",
                principalTable: "ProjectCampaign",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
