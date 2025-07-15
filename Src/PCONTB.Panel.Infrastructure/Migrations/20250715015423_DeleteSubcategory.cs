using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCONTB.Panel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeleteSubcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_CategorySubcategory_SubcategoryId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "CategorySubcategory");

            migrationBuilder.DropIndex(
                name: "IX_Project_SubcategoryId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "SubcategoryId",
                table: "Project");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubcategoryId",
                table: "Project",
                type: "uuid",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Project_SubcategoryId",
                table: "Project",
                column: "SubcategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategorySubcategory_CategoryId",
                table: "CategorySubcategory",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_CategorySubcategory_SubcategoryId",
                table: "Project",
                column: "SubcategoryId",
                principalTable: "CategorySubcategory",
                principalColumn: "Id");
        }
    }
}
