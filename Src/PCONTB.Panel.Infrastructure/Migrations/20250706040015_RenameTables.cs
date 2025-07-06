using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCONTB.Panel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_Subcategory_SubcategoryId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "Collaborator");

            migrationBuilder.DropTable(
                name: "Subcategory");

            migrationBuilder.CreateTable(
                name: "CategorySubcategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySubcategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategorySubcategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCollaborator",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ManageProjectPermission = table.Column<bool>(type: "boolean", nullable: false),
                    ManageCommunityPermission = table.Column<bool>(type: "boolean", nullable: false),
                    ManageFulfillmentPermission = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCollaborator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectCollaborator_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectCollaborator_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategorySubcategory_CategoryId",
                table: "CategorySubcategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCollaborator_ProjectId",
                table: "ProjectCollaborator",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCollaborator_UserId",
                table: "ProjectCollaborator",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_CategorySubcategory_SubcategoryId",
                table: "Project",
                column: "SubcategoryId",
                principalTable: "CategorySubcategory",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_CategorySubcategory_SubcategoryId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "CategorySubcategory");

            migrationBuilder.DropTable(
                name: "ProjectCollaborator");

            migrationBuilder.CreateTable(
                name: "Collaborator",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ManageCommunityPermission = table.Column<bool>(type: "boolean", nullable: false),
                    ManageFulfillmentPermission = table.Column<bool>(type: "boolean", nullable: false),
                    ManageProjectPermission = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collaborator_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Collaborator_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subcategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subcategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborator_ProjectId",
                table: "Collaborator",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborator_UserId",
                table: "Collaborator",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategory_CategoryId",
                table: "Subcategory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Subcategory_SubcategoryId",
                table: "Project",
                column: "SubcategoryId",
                principalTable: "Subcategory",
                principalColumn: "Id");
        }
    }
}
